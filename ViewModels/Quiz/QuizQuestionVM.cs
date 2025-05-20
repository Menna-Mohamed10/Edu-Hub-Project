namespace LMS_MVC_.ViewModels.Quiz
{
    public class QuizQuestionVM
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; } // Index of correct answer in Options

        // Temporary property to capture API data
        public List<QuestionAnswerVM> Answers { get; set; } = new List<QuestionAnswerVM>();
    }
}
