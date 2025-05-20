namespace LMS_MVC_.ViewModels.Quiz
{
    public class QuizSubmissionVM
    {
        public int QuizId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    }
}
