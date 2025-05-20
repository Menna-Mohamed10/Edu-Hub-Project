using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.AnswerDtos;

namespace LMS.BLL.Dtos.QuestionDtos
{
    public class QuizQuestionAddDto
    {
        public string QuestionText { get; set; }
        public List<QuestionAnswerAddDto> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
