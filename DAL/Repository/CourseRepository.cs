using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data;
using LMS.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LMSContext _context;
        public CourseRepository(LMSContext context)
        {
            _context = context;
        }
        public void Delete(Course course)
        {
            _context.Remove(course);
        }

        public IQueryable<Course> GetAll()
        {
            return _context.Courses.AsNoTracking();
        }

        public Course GetById(int id)
        {
            return _context.Courses.Find(id);
        }

        public int GetNumberOfStudents(Course course)
        {
            return course.NumberOfStudents;
        }

        public Course GetByName(string name)
        {
            return _context.Courses.FirstOrDefault(a => a.Title.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Course> GetByCategory(int categoryId)
        {
            return _context.Courses.Where(a => a.CategoryId == categoryId).ToList();
        }

        public IQueryable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments;
        }

        public void InsertEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
        }

        public void Insert(Course course)
        {
            _context.Add(course);
        }

     

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Course course)
        {

        }
    }
}


