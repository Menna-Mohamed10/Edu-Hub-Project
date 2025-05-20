using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.QuestionDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Dtos.QuizDtos
{
    public class QuizReadDto
    {
        public int Id { get; set; }
        //public int CourseId { get; set; }
        //public string CourseName { get; set; }
        public string Title { get; set; }
        public List<QuizQuestionReadDto> Questions { get; set; }
    }
}
