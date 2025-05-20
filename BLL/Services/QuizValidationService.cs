using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;

namespace LMS.BLL.Services
{

    public class QuizValidationService : IQuizValidationService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizValidationService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<QuizValidationResult> ValidateQuizSubmission(int quizId, Dictionary<int, string> answers)
        {
            Console.WriteLine($"Attempting to retrieve quiz with ID: {quizId}");
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (quiz == null)
            {
                Console.WriteLine($"Quiz with ID {quizId} not found.");
                return new QuizValidationResult { IsValid = false, ErrorMessage = "Quiz not found" };
            }

            var questions = await _quizRepository.GetQuestionsByQuizIdAsync(quizId);
            if (!questions.Any())
            {
                return new QuizValidationResult { IsValid = false, ErrorMessage = "No questions found for this quiz" };
            }

            var questionIds = questions.Select(q => q.Id).ToList();
            var invalidQuestionIds = answers.Keys.Where(id => !questionIds.Contains(id)).ToList();
            if (invalidQuestionIds.Any())
            {
                return new QuizValidationResult { IsValid = false, ErrorMessage = $"Invalid question IDs: {string.Join(", ", invalidQuestionIds)}" };
            }

            int score = 0;
            int totalQuestions = questions.Count();
            foreach (var question in questions)
            {
                if (answers.TryGetValue(question.Id, out var submittedAnswer))
                {
                    if (submittedAnswer == question.CorrectAnswer)
                    {
                        score++;
                    }
                }
            }

            int scorePercentage = (int)((double)score / totalQuestions * 100);
            return new QuizValidationResult { IsValid = true, Score = scorePercentage };
        }


        //public async Task<int> ValidateQuizSubmission(int quizId, Dictionary<int, string> answers)
        //{
        //    var quiz = await _quizRepository.GetQuizByIdAsync(quizId);

        //    if (quiz == null)
        //    {
        //        throw new ArgumentException("Quiz not found");
        //    }

        //    var questions = await _quizRepository.GetQuestionsByQuizIdAsync(quizId);
        //    var correctAnswersCount = 0;
        //    var totalQuestions = questions.Count();

        //    foreach (var question in questions)
        //    {
        //        if (answers.TryGetValue(question.Id, out var selectedAnswer) &&
        //            selectedAnswer == question.CorrectAnswer)
        //        {
        //            correctAnswersCount++;
        //        }
        //    }


        //    int score = totalQuestions > 0 ? (correctAnswersCount * 100) / totalQuestions : 0;
        //    return score;
        //}
    }
}
