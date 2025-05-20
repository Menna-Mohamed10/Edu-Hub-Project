using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;

namespace LMS.DAL.Repository
{
    public interface IQuizResultRepository
    {


        Task<IEnumerable<QuizResult>> GetAllResultsAsync();
        Task<QuizResult> GetResultAsync(int userId, int quizId);
        Task<IEnumerable<QuizResult>> GetResultsByUserIdAsync(int userId);
        Task<IEnumerable<QuizResult>> GetResultsByQuizIdAsync(int quizId);
        Task AddResultAsync(QuizResult result);
        void RemoveResult(QuizResult result);
        Task SaveAsync();

    }
}
