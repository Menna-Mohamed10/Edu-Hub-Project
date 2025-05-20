namespace LMS_MVC_.ViewModels.Courses
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseVM> Courses { get; set; }
    }
}
