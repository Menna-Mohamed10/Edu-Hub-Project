namespace LMS_MVC_.ViewModels.Quiz
{
    public class QuizVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<QuizQuestionVM> Questions { get; set; } = new List<QuizQuestionVM>();
    }
}
