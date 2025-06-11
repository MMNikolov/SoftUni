using Calisthenix.Server.Models;

namespace Calisthenix.Server.Services
{
    public interface IExerciseService
    {
        
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        
        Task<Exercise?> GetExerciseByIdAsync(int id);
        
        Task<Exercise> AddExerciseAsync(Exercise exercise);
        
        Task<Exercise> UpdateExerciseAsync(Exercise exercise);
        
        Task DeleteExerciseAsync(int id);
    }
}
