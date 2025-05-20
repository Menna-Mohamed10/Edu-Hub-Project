using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data.Models;
using LMS.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repository
{
    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly LMSContext _context;

        public QuizResultRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizResult>> GetAllResultsAsync()
        {
            return await _context.QuizResults
                                 .Include(r => r.User)
                                 .Include(r => r.Quiz)
                                 .ToListAsync();
        }

        public async Task<QuizResult> GetResultAsync(int userId, int quizId)
        {
            return await _context.QuizResults
                                 .Include(r => r.User)
                                 .Include(r => r.Quiz)
                                 .FirstOrDefaultAsync(r => r.UserId == userId && r.QuizId == quizId);
        }

        public async Task<IEnumerable<QuizResult>> GetResultsByUserIdAsync(int userId)
        {
            return await _context.QuizResults
                                 .Include(r => r.Quiz)
                                 .Where(r => r.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<QuizResult>> GetResultsByQuizIdAsync(int quizId)
        {
            return await _context.QuizResults
                                 .Include(r => r.User)
                                 .Where(r => r.QuizId == quizId)
                                 .ToListAsync();
        }

        public async Task AddResultAsync(QuizResult result)
        {
            await _context.QuizResults.AddAsync(result);
        }

        public void RemoveResult(QuizResult result)
        {
            _context.QuizResults.Remove(result);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
