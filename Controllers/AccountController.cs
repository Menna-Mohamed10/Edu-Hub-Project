using LMS_MVC_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static LMS_MVC_.Helper_Classes.Helper_Class;

namespace LMS_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5179/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            if (errorResponse?.Errors == null)
            {
                ModelState.AddModelError("", "Unknown error occurred");
                return View(model);
            }

            foreach (var error in errorResponse.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                HttpContext.Session.SetString("JwtToken", loginResult.Token);

                // Extract role from JWT token
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(loginResult.Token);

                // Log all claims for debugging
                var claims = token.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
                System.Diagnostics.Debug.WriteLine("JWT Claims: " + string.Join(", ", claims));

                // Try different claim types for role
                var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "role")?.Value
                             ?? token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                             ?? token.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                System.Diagnostics.Debug.WriteLine($"Role Claim: {roleClaim ?? "null"}");

                if (string.Equals(roleClaim, "Student", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("StudentDashboard", "Student");
                }
                else if (string.Equals(roleClaim, "Instructor", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("ProfessorDashboard", "Professor");
                }
                else if (string.Equals(roleClaim, "Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index", "Admin");
                }

                // Log fallback redirection
                System.Diagnostics.Debug.WriteLine("Redirecting to Home: No valid role found");
                return RedirectToAction("Index", "Home");
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            if (errorResponse?.Errors == null)
            {
                ModelState.AddModelError("", "Unknown error occurred");
                return View(model);
            }

            foreach (var error in errorResponse.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}