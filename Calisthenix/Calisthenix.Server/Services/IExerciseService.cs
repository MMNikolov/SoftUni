using Calisthenix.Server.Models;

namespace Calisthenix.Server.Services
{
    public interface IExerciseService
    {
        /// <summary>
        /// Retrieves a list of all exercises.
        /// </summary>
        /// <returns>A list of exercises.</returns>
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        /// <summary>
        /// Retrieves an exercise by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercise.</param>
        /// <returns>The exercise with the specified ID, or null if not found.</returns>
        Task<Exercise?> GetExerciseByIdAsync(int id);
        /// <summary>
        /// Adds a new exercise.
        /// </summary>
        /// <param name="exercise">The exercise to add.</param>
        /// <returns>The added exercise.</returns>
        Task<Exercise> AddExerciseAsync(Exercise exercise);
        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        /// <param name="exercise">The exercise with updated information.</param>
        /// <returns>The updated exercise.</returns>
        Task<Exercise> UpdateExerciseAsync(Exercise exercise);
        /// <summary>
        /// Deletes an exercise by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercise to delete.</param>
        Task DeleteExerciseAsync(int id);
    }
}
