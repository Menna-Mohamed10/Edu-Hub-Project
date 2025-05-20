using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels.Quiz
{
    public class QuizQuestionAddVM
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        public List<QuestionAnswerAddVM> Answers { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }
    }

    
}
