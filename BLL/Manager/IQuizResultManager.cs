using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.QuizResultDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Manager
{
    public interface IQuizResultManager
    {
        Task<IEnumerable<QuizResultReadDto>> GetAllResultsAsync();
        Task<QuizResultReadDto> GetResultAsync(int userId, int quizId);
        Task<IEnumerable<QuizResultReadDto>> GetResultsByUserIdAsync(int userId);
        Task<IEnumerable<QuizResultReadDto>> GetResultsByQuizIdAsync(int quizId);
        Task AddResultAsync(QuizResultAddDto result);
        Task RemoveResult(int userId, int quizId);

        
    }
}
