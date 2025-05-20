using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.AnswerDtos;

namespace LMS.BLL.Dtos.QuestionDtos
{
    public class QuizQuestionReadDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<QuestionAnswerReadDto> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
}
