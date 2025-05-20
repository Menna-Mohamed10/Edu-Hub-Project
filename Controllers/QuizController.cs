using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using LMS_MVC_.ViewModels.Quiz;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace LMS_MVC_.Controllers
{
    public class QuizController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuizController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5179/");
        }

        public async Task<IActionResult> TakeQuiz(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Please log in to take the quiz.";
                return RedirectToAction("Login", "Account");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"api/Quiz/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var quizData = JsonSerializer.Deserialize<QuizVM>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Map answers to options and set correct answer index
                foreach (var question in quizData.Questions)
                {
                    // Map Answers to Options
                    if (question.Answers != null && question.Answers.Any())
                    {
                        question.Options = question.Answers.Select(a => a.AnswerText).ToList();
                    }
                    else
                    {
                        question.Options = new List<string> { "No options available" };
                        question.CorrectAnswerIndex = 0;
                        continue;
                    }

                    // Validate CorrectAnswerIndex
                    if (question.CorrectAnswerIndex < 0 || question.CorrectAnswerIndex >= question.Options.Count)
                    {
                        question.CorrectAnswerIndex = 0; // Default to first option if invalid
                    }
                }

                return View(quizData);
            }

            TempData["Error"] = "Failed to load quiz. Please try again.";
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizzesByCourseId( int courseId)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Please log in to view quizzes." });
            }

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"api/Quiz/course/{courseId}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response for courseId {courseId}: {content}"); // Log raw response
                var quizzes = JsonSerializer.Deserialize<IEnumerable<QuizVM>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Json(quizzes);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error for courseId {courseId}: {ex.Message}");
                return Json(new { success = false, message = $"Failed to fetch quizzes: {ex.Message}" });
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Error for courseId {courseId}: {ex.Message}");
                return Json(new { success = false, message = $"Invalid data format: {ex.Message}" });
            }
        }



        [HttpPost]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmissionVM submission)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Please log in to submit the quiz." });
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(submission), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/QuizSubmission/SubmitQuiz", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<QuizResult>();
                return Json(new { success = true, score = result?.Score });
            }

            // Capture the detailed error message from the API
            var errorMessage = await response.Content.ReadAsStringAsync();
            return Json(new { success = false, message = $"Failed to submit quiz. API error: {errorMessage}" });
        }
    }
}