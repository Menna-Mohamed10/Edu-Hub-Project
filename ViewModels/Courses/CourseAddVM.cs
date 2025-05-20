using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels.Courses
{
    public class CourseAddVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        
        public string CategoryName { get; set; }

        public int DurationHours { get; set; }
        public string ImagePath { get; set; }
    }
}
