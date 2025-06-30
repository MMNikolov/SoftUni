namespace Calisthenix.Server.Services.Interfaces
{
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Models.DTOs;

    public interface IExerciseService
    {
        
        Task<IEnumerable<ExerciseDTO>> GetAllExercisesAsync();
        
        Task<Exercise?> GetExerciseByIdAsync(string id);
        
        Task AddExerciseAsync(Exercise exercise);
        
        Task UpdateExerciseAsync(string id, Exercise exercise);
        
        Task DeleteExerciseAsync(string id);
    }
}
