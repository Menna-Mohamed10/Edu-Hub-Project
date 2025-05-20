using LMS.BLL.Dtos.QuizDtos;
using LMS.BLL.Dtos.QuizResultDtos;
using LMS.BLL.Manager;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizManager _quizManager;
        private readonly IQuizResultManager _quizResultManager;

        public QuizController(IQuizManager quizManager, IQuizResultManager quizResultManager)
        {
            _quizManager = quizManager;
            _quizResultManager = quizResultManager;
        }

        

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<QuizReadDto>> GetQuiz(int id)
        {
            var quiz = await _quizManager.GetQuizByIdAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        [HttpGet("course/{courseId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<QuizReadDto>>> GetQuizzesByCourse(int courseId)
        {
            var quizzes = await _quizManager.GetQuizzesByCourseIdAsync(courseId);
            return Ok(quizzes);
        }


        [HttpPost("submit")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> SubmitQuiz([FromBody] QuizResultAddDto result)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            result.UserId = userId;
            result.CompletionDate = DateTime.UtcNow;

            await _quizResultManager.AddResultAsync(result);

            return Ok();
        }

        [HttpGet("results/my")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<QuizResultReadDto>>> GetMyResults()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var results = await _quizResultManager.GetResultsByUserIdAsync(userId);
            return Ok(results);
        }

        // Instructor and Admin endpoints

        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        public ActionResult CreateQuiz([FromBody] QuizAddDto quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _quizManager.AddQuizAsync(quiz);
            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.CourseId }, quiz);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor,Admin")]
        public ActionResult UpdateQuiz(int id, [FromBody] QuizUpdateDto quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _quizManager.UpdateQuiz(quiz);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteQuiz(int id)
        {
            _quizManager.DeleteQuiz(id);
            return NoContent();
        }

        

        [HttpGet("results/quiz/{quizId}")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<ActionResult<IEnumerable<QuizResultReadDto>>> GetQuizResults(int quizId)
        {
            var results = await _quizResultManager.GetResultsByQuizIdAsync(quizId);
            return Ok(results);
        }

        [HttpGet("results/all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<QuizResultReadDto>>> GetAllResults()
        {
            var results = await _quizResultManager.GetAllResultsAsync();
            return Ok(results);
        }
    }
}
