using LMS.BLL.Dtos.CategoryDto;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_categoryManager.GetAll());
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public IActionResult GetById(int Id)
        {
            return Ok(_categoryManager.GetById(Id));
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public IActionResult GetByName(string name)
        {
            return Ok(_categoryManager.GetByName(name));
        }

        


        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Insert(CategoryAddDto category)
        {
            _categoryManager.Insert(category);
            return Ok("Category created");
        }

        //[HttpPut("{Id}")]
        //[Authorize(Roles = "Admin,Instructor")]
        //public IActionResult Update(int Id, CourseUpdateDto course)
        //{
        //    if (Id != course.Id)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        _categoryManager.Update(course);
        //        return NoContent();
        //    }
        //}

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Id)
        {
            _categoryManager.Delete(Id);
            return NoContent();
        }
    }
}
