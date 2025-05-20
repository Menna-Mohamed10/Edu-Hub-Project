using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.AccountDtos;

namespace LMS.BLL.Manager
{
    public interface IAccountManager
    {
        Task<AuthResult> Register(RegisterDto registerDto);
        Task<AuthResult> Login(LoginDto loginDto );
        Task LogoutAsync();
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
