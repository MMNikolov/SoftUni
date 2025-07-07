namespace Calisthenix.Server.Services.Interfaces
{
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Models.DTOs;

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
        Task<List<WorkoutDTO>> GetWorkoutsByUserIdAsync(string userId);
        Task<WorkoutDTO> CreateWorkoutAsync(string userId, CreateWorkoutDTO dto);
        Task<bool> AddExerciseToWorkoutAsync(int workoutId, int exerciseId, string userId);
        Task<bool> RemoveExerciseFromWorkoutAsync(int workoutId, int exerciseId, string userId);
    }
}
