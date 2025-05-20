using LMS_MVC_.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMS_MVC_.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(HttpClient httpClient, IConfiguration configuration, ILogger<CategoriesController> logger)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5179/api/";
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryVM> categories = new List<CategoryVM>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}Category");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var deserializedCategories = JsonConvert.DeserializeObject<List<CategoryVM>>(json);
                    if (deserializedCategories != null)
                    {
                        categories = deserializedCategories;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while fetching categories: {ex.Message}");
            }

            return View(categories);
        }

        public async Task<IActionResult> Courses(int id)
        {
            List<CourseVM> courses = new List<CourseVM>();
            CategoryVM category = null;
            

            try
            {
                // First get the category to display its name
                HttpResponseMessage categoryResponse = await _httpClient.GetAsync($"{_apiBaseUrl}Category/{id}");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    string categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<CategoryVM>(categoryJson);
                }

                // Then get all courses in that category
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}Courses/category/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var deserializedCourses = JsonConvert.DeserializeObject<List<CourseVM>>(json);
                    if (deserializedCourses != null)
                    {
                        courses = deserializedCourses;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while fetching courses by category: {ex.Message}");
            }

            ViewBag.CategoryName = category?.Name ?? "Unknown Category";
            ViewBag.CategoryId = id;

            return View(courses);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
