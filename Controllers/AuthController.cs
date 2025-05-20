using LMS.BLL.Dtos.AccountDtos;
using LMS.BLL.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AuthController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var result = await _accountManager.Register(model);

            if (!result.Success)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(new
            {
                Message = "User created successfully!",
                Username = model.Username,
                Role = model.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _accountManager.Login(model);

            if (!result.Success)
            {
                return Unauthorized(new { Errors = result.Errors });
            }

            return Ok(new
            {
                Token = result.Token,
                Expiration = result.Expiration,
                Roles = result.Roles,
                Username = result.Username
            });
        }

        //[HttpPost("logout")]
        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    await _accountManager.LogoutAsync();
        //    return Ok(new { Message = "Logged out successfully" });
        //}
    }
}
