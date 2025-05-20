using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.DAL.Repository
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizAsync();
        Task<Quiz> GetQuizByIdAsync(int id);
        Task<IEnumerable<Quiz>> GetQuizzesByCourseIdAsync(int courseId);
        void AddQuizAsync(Quiz quiz);
        void UpdateQuiz(Quiz quiz);
        void DeleteQuiz(Quiz quiz);
        Task SaveAsync();


        // QuizQuestion Operations
      
        Task<IEnumerable<QuizQuestion>> GetQuestionsByQuizIdAsync(int quizId);
        Task AddQuestionAsync(QuizQuestion question);
        void UpdateQuestion(QuizQuestion question);
        void DeleteQuestion(QuizQuestion question);

        // QuestionAnswer Operations (if using separate table)
        Task AddAnswerAsync(QuestionAnswer answer);
        Task<IEnumerable<QuestionAnswer>> GetAnswersByQuestionIdAsync(int questionId);

        // Bulk Operations
        Task AddQuestionsToQuizAsync(int quizId, IEnumerable<QuizQuestion> questions);

       


    }
}
