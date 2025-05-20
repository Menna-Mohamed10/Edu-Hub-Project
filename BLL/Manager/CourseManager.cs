using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BLL.Dtos.CourseDtos;
using LMS.DAL.Data;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;
using Microsoft.EntityFrameworkCore;


namespace LMS.BLL.Manager
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<User> _userRepository;
        private readonly LMSContext _context;

        public CourseManager(IMapper mapper, ICourseRepository repository, ICategoryRepository categoryRepository, IRepository<User> userRepository, LMSContext context)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _context = context;
        }
        public void Delete(int Id)
        {
            var course = _repository.GetById(Id);
            if (course != null)
            {
                _repository.Delete(course);
                _repository.SaveChanges();
            }
            else throw new Exception("empty course");

        }

        public IEnumerable<CourseReadDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CourseReadDto>>(_repository.GetAll().ToList());
        }

        public IEnumerable<CourseReadDto> GetBycategory(int categoryId)
        {
            return _mapper.Map<IEnumerable<CourseReadDto>>(_repository.GetByCategory(categoryId).ToList());
        }

        public CourseReadDto GetById(int id)
        {
            return _mapper.Map<CourseReadDto>(_repository.GetById(id));
        }

        public CourseReadDto GetByName(string name)
        {
            return _mapper.Map<CourseReadDto>(_repository.GetByName(name));
        }

        public void Insert(CourseAddDto courseDto)
        {
            if (string.IsNullOrWhiteSpace(courseDto.CategoryName))
            {
                throw new ArgumentException("Category name is required.");
            }

            try
            {
                // Map the DTO to a Course entity
                var course = _mapper.Map<Course>(courseDto);

                // Find or create the category
                var category = _categoryRepository.GetByName(courseDto.CategoryName);
                if (category == null)
                {
                    category = new Category { Name = courseDto.CategoryName };
                    _categoryRepository.Insert(category);
                }

                // Link the course to the category
                course.Category = category; // Set navigation property
                course.CategoryId = category.Id; // Ensure CategoryId is set

                // Insert the course
                _repository.Insert(course);

                // Save all changes in a single transaction
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database errors (e.g., unique constraint violation)
                throw new InvalidOperationException("An error occurred while adding the course. Please check if the category or course details are valid.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while adding the course.", ex);
            }
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public void Update(CourseUpdateDto course)
        {
            _repository.Update(_mapper.Map<CourseUpdateDto, Course>(course, _repository.GetById(course.Id)));
            _repository.SaveChanges();
        }
        public void EnrollStudent(int userId, int courseId)
        {
            // Validate user
            var user = _userRepository.GetById(userId);
            if (user == null || user.Role != "Student")
                throw new InvalidOperationException("User is not a valid student.");

            // Validate course
            var course = _repository.GetById(courseId);
            if (course == null)
                throw new InvalidOperationException("Course not found.");

            // Check if already enrolled
            var existingEnrollment = _repository.GetAllEnrollments()
                .FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId);
            if (existingEnrollment != null)
                throw new InvalidOperationException("Student is already enrolled in this course.");

            // Create enrollment
            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow
            };

            // Update NumberOfStudents
            course.NumberOfStudents++;
            _repository.Update(course);

            // Add enrollment
            _repository.InsertEnrollment(enrollment);
            _repository.SaveChanges();
        }
        public IEnumerable<EnrolledCourseDto> GetEnrolledCourses(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null || user.Role != "Student")
                throw new InvalidOperationException("User is not a valid student.");

            var enrollments = _repository.GetAllEnrollments()
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ThenInclude(c => c.Category)
                .ToList();

            return enrollments.Select(e => new EnrolledCourseDto
            {
                Id = e.Course.Id,
                Title = e.Course.Title,
                Price = e.Course.Price,
                Category = e.Course.Category?.Name,
                DurationHours = e.Course.DurationHours,
                ImagePath = e.Course.ImagePath,
                EnrollmentDate = e.EnrollmentDate
            });
        }


    }
}
