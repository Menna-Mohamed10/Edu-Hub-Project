using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.DAL.Repository
{
    public interface ICourseRepository
    {
        IQueryable<Course> GetAll();
        Course GetById(int id);
        Course GetByName(string name);
        IEnumerable<Course> GetByCategory(int categoryId);
        int GetNumberOfStudents(Course course);
        IQueryable<Enrollment> GetAllEnrollments();
        void InsertEnrollment(Enrollment enrollment);
        void Insert(Course course);
        void Update(Course course);
        void Delete(Course course);
        void SaveChanges();

    }
}
