using Calisthenix.Server.Models;

namespace Calisthenix.Server.Services.Interfaces
{
    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetAllAsync(int userId);
        Task<Workout?> GetByIdAsync(int id, int userId);
        Task<Workout> CreateAsync(Workout workout, int userId);
        Task<bool> DeleteAsync(int id, int userId);
        Task<Workout> GetOrCreateDefaultWorkoutAsync(int userId);
        Task<bool> AddExerciseToWorkoutAsync(int workoutId, int exerciseId);
        Task AddExerciseToUserWorkoutAsync(int userId, int exerciseId);
        User GetUserByUsername(string username);
    }
}
