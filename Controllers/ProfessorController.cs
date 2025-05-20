using System.Text.Json;
using LMS_MVC_.ViewModels.Courses;
using LMS_MVC_.ViewModels.Quiz;
using Microsoft.AspNetCore.Mvc;

namespace LMS_MVC_.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly HttpClient _httpClient;
       
        
        public ProfessorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5179/");
        }

        public IActionResult ProfessorDashboard()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Please log in to access the professor dashboard.";
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseAddVM course)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Please log in as a professor.";
                
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(course), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Courses", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Course added successfully.";
                return Json(new { success = true });
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"Failed to add course. API error: {errorMessage}";
            return Json(new { success = false, message = TempData["Error"]?.ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> AddQuiz([FromBody] QuizAddVM quiz)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Please log in to add a quiz." });
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(quiz), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Quiz", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Quiz added successfully.";
                return Json(new { success = true });
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"Failed to add quiz. API error: {errorMessage}";
            return Json(new { success = false, message = TempData["Error"]?.ToString() });
        }
    }
}
