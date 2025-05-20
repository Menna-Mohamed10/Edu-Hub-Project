using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BLL.Dtos.CourseDtos;
using LMS.BLL.Dtos.QuizDtos;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMS.BLL.Manager
{
    public class QuizManager : IQuizManager
    {
        private readonly IQuizRepository _repository;
        private readonly IMapper _mapper;
        private readonly IQuizResultRepository _quizResultRepository;

        public QuizManager(IQuizRepository repository, IMapper mapper, IQuizResultRepository quizResultRepository) 
        { 
            _repository = repository;
            _mapper = mapper;
            _quizResultRepository = quizResultRepository;
        }
        public void AddQuizAsync(QuizAddDto quiz)
        {
            _repository.AddQuizAsync(_mapper.Map<Quiz>(quiz));
            _repository.SaveAsync();
        }

        public async Task DeleteQuiz(int id)
        {
            var quiz = await _repository.GetQuizByIdAsync(id);
            if (quiz != null)
            {
                _repository.DeleteQuiz(quiz);
                _repository.SaveAsync();
            }
            else throw new Exception("empty quiz");

        }

        public async Task<IEnumerable<QuizReadDto>> GetAllQuizAsync()
        {
            var quizzes = await _repository.GetAllQuizAsync();
            return _mapper.Map<IEnumerable<QuizReadDto>>(quizzes.ToList());
        }

        public async Task<QuizReadDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _repository.GetQuizByIdAsync(id);
            var quizDto = _mapper.Map<QuizReadDto>(quiz);
            foreach (var question in quizDto.Questions)
            {
                var answers = question.Answers.Select(a => a.AnswerText).ToList();
                question.CorrectAnswerIndex = answers.IndexOf(quiz.Questions.FirstOrDefault(q => q.Id == question.Id)?.CorrectAnswer) ;
                if (question.CorrectAnswerIndex < 0 || question.CorrectAnswerIndex >= answers.Count)
                {
                    question.CorrectAnswerIndex = 0;
                }
            }
            return quizDto;
        }

        public async Task<IEnumerable<QuizReadDto>> GetQuizzesByCourseIdAsync(int courseId)
        {
            var quizes = await _repository.GetQuizzesByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<QuizReadDto>>(quizes.ToList());
        }



        public void UpdateQuiz(QuizUpdateDto quiz)
        {
            _repository.UpdateQuiz(_mapper.Map<Quiz>(quiz));
            _repository.SaveAsync();
        }

        public void SubmitQuizResultAsync(int userId, int quizId, int score)
        {
            var quizResult = new QuizResult
            {
                UserId = userId,
                QuizId = quizId,
                Score = score,
                CompletionDate = DateTime.UtcNow
            };

            _quizResultRepository.AddResultAsync(quizResult);
            _quizResultRepository.SaveAsync();
        }
    }
}
