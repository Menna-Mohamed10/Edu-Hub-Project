using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Dtos.CourseDtos
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public string Category { get; set; }
        public int NumberOfStudents { get; set; }
        public int DurationHours { get; set; }
        public string ImagePath { get; set; }
    }
}
