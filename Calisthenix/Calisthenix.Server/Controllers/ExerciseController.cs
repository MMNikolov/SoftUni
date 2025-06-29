namespace Calisthenix.Server.Controllers
{
    using Calisthenix.Server.Data;
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly CalisthenixDbContext _context;
        private readonly IExerciseService _exerciseService;
        public ExerciseController(IExerciseService exerciseService, CalisthenixDbContext context)
        {
            _exerciseService = exerciseService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _exerciseService.GetAllExercisesAsync();

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

            if (string.IsNullOrEmpty(exercise.Name) || string.IsNullOrEmpty(exercise.Description))
            {
                return BadRequest("Exercise data is incomplete.");
            }

            return Ok(exercise);
        }

        [HttpGet("mine")]
        [Authorize]
        public async Task<IActionResult> GetMyWorkouts()
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return Unauthorized();

            var exercises = await _context.Exercises
                .Where(e => e.UserId == user.Id)
                .ToListAsync();

            return Ok(exercises);
        }


        [HttpPost]
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

            await _exerciseService.AddExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(string id, [FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingExercise = 
                await _exerciseService.GetExerciseByIdAsync(id);

            if (existingExercise == null)
            {
                return NotFound();
            }

            await _exerciseService.UpdateExerciseAsync(id, exercise);
            return NoContent();
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(string id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}
