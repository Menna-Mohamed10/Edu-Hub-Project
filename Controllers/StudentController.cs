using LMS.BLL.Dtos.CourseDtos;
using LMS.BLL.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly ICourseManager _courseManager;

        public StudentController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }

        [HttpGet("EnrolledCourses")]
        public IActionResult GetEnrolledCourses()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new InvalidOperationException("User ID not found."));
                var courses = _courseManager.GetEnrolledCourses(userId);
                return Ok(courses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred." });
            }
        }
    }
}
