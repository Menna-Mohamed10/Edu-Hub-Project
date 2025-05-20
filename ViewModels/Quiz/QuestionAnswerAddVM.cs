using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels.Quiz
{
    public class QuestionAnswerAddVM
    {
        [Required]
        public string AnswerText { get; set; }
    }
}
