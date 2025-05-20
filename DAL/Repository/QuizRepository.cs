using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DAL.Data;
using LMS.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.DAL.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly LMSContext _context;
        public QuizRepository(LMSContext context) 
        {
            _context = context;
        }

        public async Task AddAnswerAsync(QuestionAnswer answer)
        {
            await _context.QuestionAnswers.AddAsync(answer);
        }

        public async Task AddQuestionAsync(QuizQuestion question)
        {
            await _context.QuizQuestions.AddAsync(question);
        }

        public async Task AddQuestionsToQuizAsync(int quizId, IEnumerable<QuizQuestion> questions)
        {
            var quiz = await GetQuizByIdAsync(quizId);
            if (quiz != null)
            {
                foreach (var question in questions)
                {
                    question.QuizId = quizId;
                    await AddQuestionAsync(question);
                }
                await _context.SaveChangesAsync();
            }
        }

        public void AddQuizAsync(Quiz quiz)
        {
            _context.Add(quiz);
        }

        public void DeleteQuestion(QuizQuestion question)
        {
            _context.QuizQuestions.Remove(question);
        }

        public void DeleteQuiz(Quiz quiz)
        {
            _context.Remove(quiz);
        }

        

        public async Task<IEnumerable<Quiz>> GetAllQuizAsync()
        {
            return await _context.Quizes
                                 .Include(q => q.Course)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAnswersByQuestionIdAsync(int questionId)
        {
            return await _context.QuestionAnswers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<QuizQuestion>> GetQuestionsByQuizIdAsync(int quizId)
        {
            return await _context.QuizQuestions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(int id)
        {
            return await _context.Quizes
                                 .Include(q => q.Course)
                                 .Include(q => q.Questions)
                                    .ThenInclude(q => q.Answers)
                                 .Include(q => q.QuizResults)
                                 .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesByCourseIdAsync(int courseId)
        {
            
            return await _context.Quizes
                                       .Where(q => q.CourseId == courseId)
                                       .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateQuestion(QuizQuestion question)
        {
            
        }

        public void UpdateQuiz(Quiz quiz)
        {
            
        }

        
    }
}
