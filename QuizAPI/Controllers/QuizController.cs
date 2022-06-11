using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace QuizAPI.Controllers
{
    [ApiController]
    [Route("[controller]/{id?}")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;
        private QuizAPIContext context;

        public QuizController(ILogger<QuizController> logger, QuizAPIContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Quiz>> Get(int id = -1)
        {

            return await context.Quizzes.Where(q => q.Id == id || id == -1)
                .Include(o => o.QuizQuestions)
                .ThenInclude(p => p.QuizQuestionAnswers)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> Create(Quiz quiz)
        {
            try
            {
                if (quiz is null)
                {
                    return BadRequest();
                }


                context.Quizzes.Add(quiz);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "Quiz created.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Quiz>> Update(Quiz quiz)
        {
            try
            {
                if (quiz is null)
                {
                    return BadRequest();
                }

                var quizToUpdate = await context.Quizzes
                    .AsNoTracking()
                    .Where(q => q.Id == quiz.Id)
                    .FirstOrDefaultAsync();

                if (quizToUpdate is null)
                {
                    context.Quizzes.Add(quiz);
                    await context.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status200OK, "Quiz created.");
                }
                else
                {
                    context.Quizzes.Update(quiz);
                    await context.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status200OK, "Quiz updated.");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Quiz>> Delete(int id)
        {
            try
            {
                var quizToDelete = await context.Quizzes
                    .AsNoTracking()
                    .Where(q => q.Id == id)
                    .FirstOrDefaultAsync();

                if (quizToDelete is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Quiz not found.");
                }
                else
                {
                    context.Quizzes.Remove(quizToDelete);
                    await context.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status200OK, "Quiz deleted");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error");
            }
        }
    }
}
