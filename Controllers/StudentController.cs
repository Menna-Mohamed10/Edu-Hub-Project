using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using static LMS_MVC_.Helper_Classes.Helper_Class;

namespace LMS_MVC_.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IHttpClientFactory httpClientFactory, ILogger<StudentController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5179/");
            _logger = logger;
        }

        public IActionResult StudentDashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["Error"] = "Please log in as a student to enroll.";
                    return RedirectToAction("Index", "Course");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsync($"api/Courses/{courseId}/Enroll", null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Successfully enrolled in the course.";
                    return RedirectToAction("StudentDashboard");
                }

                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                TempData["Error"] = error?.Error ?? "Failed to enroll in the course. Please log in as a student.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error enrolling in course {courseId}: {ex.Message}");
                TempData["Error"] = "An error occurred while enrolling. Please log in as a student.";
            }

            return RedirectToAction("Index", "Course");
        }
    }
}
