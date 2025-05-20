using LMS_MVC_.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace LMS_MVC_.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CourseController> _logger;
        private readonly string _apiBaseUrl;
        public CourseController(HttpClient httpClient, IConfiguration configuration, ILogger<CourseController> logger)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5179/api/"; // Better to get from configuration
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            List<CourseVM> courses = new List<CourseVM>();
            List<CategoryVM> categories = new List<CategoryVM>();

            try
            {
                // Get all courses
                HttpResponseMessage coursesResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Courses");
                if (coursesResponse.IsSuccessStatusCode)
                {
                    string json = await coursesResponse.Content.ReadAsStringAsync();
                    var deserializedCourses = JsonConvert.DeserializeObject<List<CourseVM>>(json);
                    if (deserializedCourses != null)
                    {
                        courses = deserializedCourses;
                    }
                }

                // Get all categories
                HttpResponseMessage categoriesResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Category");
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    string json = await categoriesResponse.Content.ReadAsStringAsync();
                    var deserializedCategories = JsonConvert.DeserializeObject<List<CategoryVM>>(json);
                    if (deserializedCategories != null)
                    {
                        categories = deserializedCategories;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while fetching courses and categories: {ex.Message}");
            }

            ViewBag.Categories = categories;
            return View(courses);
        }

        public async Task<IActionResult> ByCategory(int id)
        {
            List<CourseVM> courses = new List<CourseVM>();
            CategoryVM category = null;

            try
            {
                // Fetch category to get the name
                HttpResponseMessage categoryResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Category/{id}");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    string categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<CategoryVM>(categoryJson);
                }

                // Fetch courses by category
                HttpResponseMessage courseResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Courses/category/{id}");
                if (courseResponse.IsSuccessStatusCode)
                {
                    string courseJson = await courseResponse.Content.ReadAsStringAsync();
                    var deserializedCourses = JsonConvert.DeserializeObject<List<CourseVM>>(courseJson);
                    if (deserializedCourses != null)
                    {
                        courses = deserializedCourses;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while fetching category/courses: {ex.Message}");
            }

            if (category == null)
            {
                return NotFound();
            }

            ViewBag.CategoryName = category.Name;
            return View(courses);
        }
    }
}