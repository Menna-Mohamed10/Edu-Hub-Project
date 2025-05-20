using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels.Courses
{
    public class CourseVM
    {
        
        public int Id { get; set; }

        
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public string Category { get; set; }
        public int NumberOfStudents { get; set; }
        public int DurationHours { get; set; }
        public string ImagePath { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
        //public ICollection<Quiz> Quizzes { get; set; }
    }
}
