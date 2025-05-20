using LMS.BLL.Dtos.QuizResultDtos;
using LMS.BLL.Manager;
using LMS.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.BLL.Dtos;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class QuizSubmissionController : ControllerBase
    {
        private readonly IQuizValidationService _validationService;
        private readonly IQuizResultManager _resultManager;

        public QuizSubmissionController(
            IQuizValidationService validationService,
            IQuizResultManager resultManager)
        {
            _validationService = validationService;
            _resultManager = resultManager;
        }

        [HttpPost("SubmitQuiz")]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmissionDto submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                
                var score = await _validationService.ValidateQuizSubmission(
                    submission.QuizId,
                    submission.Answers
                );
                if (!score.IsValid)
                {
                    return BadRequest(score.ErrorMessage);
                }


                await _resultManager.AddResultAsync(new QuizResultAddDto
                {
                    UserId = userId,
                    QuizId = submission.QuizId,
                    Score = score.Score,
                    CompletionDate = DateTime.UtcNow
                });

                

                return Ok(new { Score = score.Score });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your submission");
            }
        }
    }
}
