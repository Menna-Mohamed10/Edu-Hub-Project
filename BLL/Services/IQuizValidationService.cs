using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;

namespace LMS.BLL.Services
{
    public interface IQuizValidationService
    {
        Task<QuizValidationResult> ValidateQuizSubmission(int quizId, Dictionary<int, string> answers);
    }

    
}
