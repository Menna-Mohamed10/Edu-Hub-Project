using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure;
using LMS.BLL.Dtos.AccountDtos;
using LMS.DAL.Data.Constant;
using LMS.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;  
using System.IdentityModel.Tokens.Jwt;
using LMS.DAL.Data;

namespace LMS.BLL.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LMSContext _context;

        public AccountManager(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            LMSContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new[] { "Invalid username or password" }
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = GetToken(authClaims);

            return new AuthResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Roles = userRoles,
                Username = user.UserName
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthResult> Register(RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new[] { "User already exists!" }
                };
            }

            if (string.IsNullOrWhiteSpace(registerDto.Role) || !UserRoles.AllRoles.Contains(registerDto.Role))
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new[] { "Invalid role provided." }
                };
            }

            User user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = registerDto.Role
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            if (!await _roleManager.RoleExistsAsync(registerDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(registerDto.Role));
            }

            await _userManager.AddToRoleAsync(user, registerDto.Role);

            // Create Student or Professor record based on role
            if (registerDto.Role == UserRoles.Student)
            {
                var student = new Student
                {
                    UserId = user.Id,
                    WalletBalance = 0.00m,
                    Certificates = ""
                };
                _context.Students.Add(student);
            }
            else if (registerDto.Role == UserRoles.Instructor)
            {
                var professor = new Professor
                {
                    UserId = user.Id,
                    Bio = ""
                };
                _context.Professors.Add(professor);
            }

            await _context.SaveChangesAsync();

            return new AuthResult { Success = true };
        }

        private JwtSecurityToken GetToken(IList<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is missing or empty in configuration. Check your appsettings.json file.");
            }

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(secretKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
