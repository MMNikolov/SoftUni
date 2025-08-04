namespace Calisthenix.Server.Controllers
{
    using Calisthenix.Server.Data;
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Models.DTOs;
    using Calisthenix.Server.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    [EnableCors("AllowVite")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly CalisthenixDbContext _context;

        public WorkoutController(IWorkoutService workoutService, CalisthenixDbContext context)
        {
            _workoutService = workoutService;
            _context = context;
        }

        private int GetUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdStr, out int userId) ? userId 
                : throw new UnauthorizedAccessException("Invalid user ID.");
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = await _workoutService.CreateWorkoutAsync(userId, dto);

            return CreatedAtAction(nameof(GetMyWorkouts), new { id = created.Id }, created);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyWorkouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workouts = await _workoutService.GetWorkoutsByUserIdAsync(userId);

            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();

            var workout = await _workoutService.GetByIdAsync(id, userId);

            if (workout == null)
                return NotFound("Workout not found!");

            return Ok(workout);
        }

        [HttpPost("add/{exerciseId}")]
        public async Task<IActionResult> AddToWorkout(int exerciseId)
        {
            var userId = GetUserId();

            await _workoutService.AddExerciseToUserWorkoutAsync(userId, exerciseId);

            return Ok(new { message = "Exercise added to workout." });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var result = await _workoutService.GetAllWorkoutsWithExercisesRawAsync(userId);
            return Ok(result);
        }


        [HttpPost("{workoutId}/exercise/{exerciseId}")]
        [Authorize]
        public async Task<IActionResult> AddExerciseToWorkout(int workoutId, int exerciseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _workoutService.AddExerciseToWorkoutAsync(workoutId, exerciseId, userId);

            if (!success)
                return BadRequest("Could not add exercise (invalid workout or duplicate).");

            return Ok("Exercise added to workout.");
        }

        [HttpDelete("{workoutId}/exercise/{exerciseId}")]
        [Authorize]
        public async Task<IActionResult> RemoveExercise(int workoutId, int exerciseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var success = await _workoutService.RemoveExerciseFromWorkoutAsync(workoutId, exerciseId, userId);

            if (!success)
                return BadRequest("Failed to remove exercise from workout.");

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWorkoutName(int id, [FromBody] WorkoutDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updated = await _workoutService.UpdateWorkoutNameAsync(id, userId, dto.Name);

            if (!updated)
                return NotFound("Workout not found or unauthorized.");

            return Ok(new { message = "Workout name updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleted = await _workoutService.DeleteAsync(id, int.Parse(userId));

            if (!deleted)
                return NotFound("Workout not found or unauthorized.");

            return NoContent();
        }

    }
}
