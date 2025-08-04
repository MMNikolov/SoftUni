namespace Calisthenix.Server.Controllers
{
    using System.Security.Claims;
    using Azure;
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllExercises([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and pageSize must be greater than 0.");

            var exercises = await _exerciseService.GetPaginatedExercisesAsync(page, pageSize);
            return Ok(exercises);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(string id)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(exercise);
            }

            if (string.IsNullOrEmpty(exercise.Name) || string.IsNullOrEmpty(exercise.Description))
            {
                return BadRequest("Exercise name and description are required.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            exercise.UserId = int.Parse(userId); 

            await _exerciseService.AddExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExercise(string id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            try
            {
                var deleted = await _exerciseService.DeleteExerciseAsync(id, userId);
                if (!deleted)
                    return Forbid();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
