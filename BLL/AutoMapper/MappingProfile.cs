using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BLL.Dtos.AnswerDtos;
using LMS.BLL.Dtos.CategoryDto;
using LMS.BLL.Dtos.CourseDtos;
using LMS.BLL.Dtos.QuestionDtos;
using LMS.BLL.Dtos.QuizDtos;
using LMS.BLL.Dtos.QuizResultDtos;
using LMS.DAL.Data.Models;



namespace LMS.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Course, CourseAddDto>().ReverseMap(); 
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            CreateMap<Course, CourseReadDto>().ReverseMap();
            CreateMap<Course, CourseSummaryDto>().ReverseMap();
            CreateMap<Course, EnrolledCourseDto>().ReverseMap();
            CreateMap<Quiz , QuizReadDto>().ReverseMap();
            CreateMap<Quiz, QuizUpdateDto>().ReverseMap();
            CreateMap<Quiz, QuizAddDto>().ReverseMap();
            CreateMap<QuizResult, QuizResultReadDto>().ReverseMap();
            CreateMap<QuizResult, QuizResultAddDto>().ReverseMap();
            CreateMap<QuizQuestion, QuizQuestionReadDto>().ReverseMap();
            CreateMap<QuizQuestion, QuizQuestionUpdateDto>().ReverseMap();
            CreateMap<QuizQuestion, QuizQuestionAddDto>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerReadDto>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerUpdateDto>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerAddDto>().ReverseMap();
            CreateMap<Category, CategoryAddDto>().ReverseMap(); 
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<Category, CategoryNameDto>().ReverseMap();
            CreateMap<Course, EnrolledCourseDto>().ReverseMap();



        }
        
    }
}
