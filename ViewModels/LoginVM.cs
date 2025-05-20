using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
