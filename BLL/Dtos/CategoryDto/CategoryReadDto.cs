using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.CourseDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Dtos.CategoryDto
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseReadDto> Courses { get; set; }
    }
}
