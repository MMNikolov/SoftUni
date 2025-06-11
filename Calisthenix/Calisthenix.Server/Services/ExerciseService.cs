using Calisthenix.Server.Models;

namespace Calisthenix.Server.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly List<Exercise> _exercises = new List<Exercise>();
        public Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return Task.FromResult<IEnumerable<Exercise>>(_exercises);
        }
        public Task<Exercise?> GetExerciseByIdAsync(int id)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(exercise);
        }
        public Task<Exercise> AddExerciseAsync(Exercise exercise)
        {
            exercise.Id = _exercises.Count > 0 ? _exercises.Max(e => e.Id) + 1 : 1;
            _exercises.Add(exercise);
            return Task.FromResult(exercise);
        }
        public Task<Exercise> UpdateExerciseAsync(Exercise exercise)
        {
            var existing = _exercises.FirstOrDefault(e => e.Id == exercise.Id);

            if (existing != null)
            {
                existing.Name = exercise.Name;
                existing.Description = exercise.Description;
                existing.Category = exercise.Category;
                existing.Equipment = exercise.Equipment;
                existing.Difficulty = exercise.Difficulty;
                existing.VideoUrl = exercise.VideoUrl;
                existing.ImageUrl = exercise.ImageUrl;
            }
            else
            {
                throw new KeyNotFoundException($"Exercise with ID {exercise.Id} not found.");
            }   

            return Task.FromResult(existing);
        }
        public Task DeleteExerciseAsync(int id)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Id == id);
            if (exercise != null)
            {
                _exercises.Remove(exercise);
            }
            return Task.CompletedTask;
        }
    }
}
