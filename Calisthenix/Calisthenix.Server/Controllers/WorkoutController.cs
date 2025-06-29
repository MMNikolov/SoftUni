using Calisthenix.Server.Data;
using Calisthenix.Server.Models;
using Calisthenix.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calisthenix.Server.Controllers
{
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

    }
}
