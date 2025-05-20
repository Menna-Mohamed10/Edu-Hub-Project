using System.Diagnostics;
using LMS_MVC_.Models;
using LMS_MVC_.ViewModels;
using LMS_MVC_.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMS_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5179/api/";
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeVM
            {
                Courses = new List<CourseVM>(),
                Categories = new List<CategoryVM>()
            };

            try
            {
                // Fetch courses
                _logger.LogInformation($"Calling API: {_apiBaseUrl}Courses");
                HttpResponseMessage courseResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Courses");
                _logger.LogInformation($"Courses API Response Status: {courseResponse.StatusCode}");

                if (courseResponse.IsSuccessStatusCode)
                {
                    string courseJson = await courseResponse.Content.ReadAsStringAsync();
                    var courses = JsonConvert.DeserializeObject<List<CourseVM>>(courseJson);
                    if (courses != null)
                    {
                        model.Courses = courses.Take(6).ToList();
                        _logger.LogInformation($"Successfully deserialized {courses.Count} courses");
                    }
                }
                else
                {
                    _logger.LogError($"Courses API request failed with status code: {courseResponse.StatusCode}");
                }

                // Fetch categories
                _logger.LogInformation($"Calling API: {_apiBaseUrl}Category");
                HttpResponseMessage categoryResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Category");
                _logger.LogInformation($"Categories API Response Status: {categoryResponse.StatusCode}");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    string categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<CategoryVM>>(categoryJson);
                    if (categories != null)
                    {
                        model.Categories = categories;
                        _logger.LogInformation($"Successfully deserialized {categories.Count} categories");
                    }
                }
                else
                {
                    _logger.LogError($"Categories API request failed with status code: {categoryResponse.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while fetching data: {ex.Message}");
            }

            return View(model);
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
