using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BLL.Dtos.QuizResultDtos;
using LMS.DAL.Data.Models;
using LMS.DAL.Repository;

namespace LMS.BLL.Manager
{
    public class QuizResultManager : IQuizResultManager
    {
        private readonly IQuizResultRepository _repository;
        private readonly IMapper _mapper;
        
        public QuizResultManager(IQuizResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task AddResultAsync(QuizResultAddDto result)
        {
            _repository.AddResultAsync(_mapper.Map<QuizResult>(result));
            _repository.SaveAsync();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<QuizResultReadDto>> GetAllResultsAsync()
        {
            var results = await _repository.GetAllResultsAsync();
            return _mapper.Map<IEnumerable<QuizResultReadDto>>(results.ToList());
        }

        public async Task<QuizResultReadDto> GetResultAsync(int userId, int quizId)
        {
            var result = await _repository.GetResultAsync(userId, quizId);
            return _mapper.Map<QuizResultReadDto>(result);
        }

        public async Task<IEnumerable<QuizResultReadDto>> GetResultsByQuizIdAsync(int quizId)
        {
            var result = await _repository.GetResultsByQuizIdAsync(quizId);
            return _mapper.Map<IEnumerable<QuizResultReadDto>>(result.ToList());
        }

        public async Task<IEnumerable<QuizResultReadDto>> GetResultsByUserIdAsync(int userId)
        {
            var result = await _repository.GetResultsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<QuizResultReadDto>>(result.ToList());
        }

        public async Task RemoveResult(int userId, int quizId)
        {
            var result = await _repository.GetResultAsync(userId, quizId);
            if (result != null)
            {
                _repository.RemoveResult(result);
                _repository.SaveAsync();
            }
        }

        
    }
}
