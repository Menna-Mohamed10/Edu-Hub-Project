using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Dtos.CourseDtos
{
    public class EnrolledCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int DurationHours { get; set; }
        public string ImagePath { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
