using System.ComponentModel.DataAnnotations;

namespace LMS_MVC_.ViewModels.Quiz
{
   
        public class QuizAddVM
        {
            [Required]
            public int CourseId { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public List<QuizQuestionAddVM> Questions { get; set; }
        }

        
    
}
