using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int NumberOfStudents { get; set; }
        public int DurationHours { get; set; }
        public string ImagePath { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
