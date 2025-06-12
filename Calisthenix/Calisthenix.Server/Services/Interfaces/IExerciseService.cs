using Calisthenix.Server.Models;

namespace Calisthenix.Server.Services.Interfaces
{
    public interface IExerciseService
    {
        
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        
        Task<Exercise?> GetExerciseByIdAsync(string id);
        
        Task AddExerciseAsync(Exercise exercise);
        
        Task UpdateExerciseAsync(string id, Exercise exercise);
        
        Task DeleteExerciseAsync(string id);
    }
}
