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
            var username = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username)
                       ?? throw new Exception("User not found");
            return user.Id;
        }

        [HttpPost("add-exercise")]
        public async Task<IActionResult> AddExerciseToWorkout([FromBody] int exerciseId)
        {
            var userId = GetUserId();

            var workout = await _workoutService.GetOrCreateDefaultWorkoutAsync(userId);
            var success = await _workoutService.AddExerciseToWorkoutAsync(workout.Id, exerciseId);

            if (!success)
                return BadRequest("Could not add exercise.");

            return Ok();
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
        public IActionResult GetAll()
        {
            var userId = GetUserId();
        
            var workouts = _context.Workouts
                .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                .Where(w => w.UserId == userId)
                .Select(w => new
                {
                    w.Id,
                    w.Name,
                    w.UserId,
                    WorkoutExercises = w.WorkoutExercises.Select(we => new
                    {
                        we.ExerciseId,
                        Exercise = new
                        {
                            we.Exercise.Id,
                            we.Exercise.Name,
                            we.Exercise.Category,
                            we.Exercise.Difficulty,
                            we.Exercise.Equipment,
                            we.Exercise.ImageUrl,
                            we.Exercise.VideoUrl
                        }
                    })
                })
                .ToList();
        
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();

            var workout = await _workoutService.GetByIdAsync(id, userId);

            if (workout == null)
                return NotFound();

            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Workout workout)
        {
            var userId = GetUserId();

            var created = await _workoutService.CreateAsync(workout, userId);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyWorkouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workouts = await _workoutService.GetWorkoutsByUserIdAsync(userId);

            return Ok(workouts);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = await _workoutService.CreateWorkoutAsync(userId, dto);

            return CreatedAtAction(nameof(GetMyWorkouts), new { id = created.Id }, created);
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

            var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId.ToString().ToLower() == userId.ToString().ToLower());
            if (workout == null) return NotFound("Workout not found");

            workout.Name = dto.Name;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Workout name updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId.ToString().ToLower() == userId.ToString().ToLower());

            if (workout == null)
                return NotFound();

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
