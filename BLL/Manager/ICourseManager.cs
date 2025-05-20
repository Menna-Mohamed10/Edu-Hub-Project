using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.CourseDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Manager
{
    public interface ICourseManager
    {
        IEnumerable<CourseReadDto> GetAll();
        CourseReadDto GetById(int id);
        CourseReadDto GetByName(string name);
        IEnumerable<CourseReadDto> GetBycategory(int categoryId);
        void Insert(CourseAddDto course);
        void Update(CourseUpdateDto course);
        void Delete(int Id);
        void EnrollStudent(int userId, int courseId);
        IEnumerable<EnrolledCourseDto> GetEnrolledCourses(int userId);
        void SaveChanges();
    }
}
