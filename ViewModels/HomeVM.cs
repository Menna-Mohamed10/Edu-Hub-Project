using System.Collections.Generic;
using LMS_MVC_.ViewModels.Courses;

namespace LMS_MVC_.ViewModels
{
    public class HomeVM
    {
        public List<CourseVM> Courses { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
