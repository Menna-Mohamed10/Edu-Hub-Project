using System.Security.Claims;
using LMS.BLL.Dtos.CourseDtos;
using LMS.BLL.Manager;
using LMS.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class CoursesController : ControllerBase
    {
        private readonly ICourseManager _courseManager;
        public CoursesController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }

        [HttpGet]
        [AllowAnonymous] 
        public IActionResult GetAll()
        {
            return Ok(_courseManager.GetAll());
        }

        [HttpGet("{Id}")]
        [AllowAnonymous] 
        public IActionResult GetById(int Id)
        {
            return Ok(_courseManager.GetById(Id));
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous] 
        public IActionResult GetByName(string name)
        {
            return Ok(_courseManager.GetByName(name));
        }

        [HttpGet("category/{Id}")]
        [AllowAnonymous]
        public IActionResult GetByCategory(int Id)
        {
            return Ok(_courseManager.GetBycategory(Id));
        }

        [HttpPost] 
        [Authorize(Roles = "Admin,Instructor")] 
        public IActionResult Insert(CourseAddDto course)
        {
            _courseManager.Insert(course);
            return Ok("Course created");
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin,Instructor")] 
        public IActionResult Update(int Id, CourseUpdateDto course)
        {
            if (Id != course.Id)
            {
                return BadRequest();
            }
            else
            {
                _courseManager.Update(course);
                return NoContent();
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Delete(int Id)
        {
            _courseManager.Delete(Id);
            return NoContent();
        }

        [HttpPost("{courseId}/Enroll")]
        [Authorize(Roles = "Student")]
        public IActionResult Enroll(int courseId)
        {
            try
            {
                // Get the authenticated user's ID from the JWT token
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new InvalidOperationException("User ID not found in token."));

                _courseManager.EnrollStudent(userId, courseId);
                return Ok("Successfully enrolled in the course.");
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