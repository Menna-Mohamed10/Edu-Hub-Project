using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Dtos.CourseDtos
{
    public class CourseUpdateDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
      
        public Category Category { get; set; }
      
        public int DurationHours { get; set; }
        public string ImagePath { get; set; }

    }
}
