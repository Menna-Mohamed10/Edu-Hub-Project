namespace LMS_MVC_.Helper_Classes
{
    public class Helper_Class
    {
        public class ErrorResponse
        {
            public IEnumerable<string> Errors { get; set; }
            public string? Error { get; set; }
        }

        public class LoginResult
        {
            public string Token { get; set; }
            public string Username { get; set; }
            public IEnumerable<string> Roles { get; set; }
            public DateTime Expiration { get; set; }
            
        }
    }
}
