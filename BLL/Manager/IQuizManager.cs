using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.BLL.Dtos.QuizDtos;
using LMS.DAL.Data.Models;

namespace LMS.BLL.Manager
{
    public interface IQuizManager
    {
        Task<IEnumerable<QuizReadDto>> GetAllQuizAsync();
        Task<QuizReadDto> GetQuizByIdAsync(int id);
        Task<IEnumerable<QuizReadDto>> GetQuizzesByCourseIdAsync(int courseId);
        void AddQuizAsync(QuizAddDto quiz);
        void UpdateQuiz(QuizUpdateDto quiz);
        Task DeleteQuiz(int id);

        void SubmitQuizResultAsync(int userId, int quizId, int score);
    }
}
