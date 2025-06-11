using Calisthenix.Server.Models;
using Calisthenix.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calisthenix.Server.Controllers
{
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
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _exerciseService.GetAllExercisesAsync();
            return Ok(exercises);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
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
        
        
        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {
            if (exercise == null)
            {
                return BadRequest("Invalid exercise data.");
            }
        
            var addedExercise = await _exerciseService.AddExerciseAsync(exercise);  
        
            if (addedExercise == null)
            {
                return BadRequest("Error adding exercise.");
            }
        
            return CreatedAtAction(nameof(GetExerciseById), new { id = addedExercise.Id }, addedExercise);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] Exercise exercise)
        {
            if (exercise == null || exercise.Id != id)
            {
                return BadRequest("Invalid exercise data.");
            }

            var updatedExercise = await _exerciseService.UpdateExerciseAsync(exercise);

            if (updatedExercise == null)
            {
                return NotFound();
            }
            return Ok(updatedExercise);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}
