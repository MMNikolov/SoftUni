using Calisthenix.Server.Data;
using Calisthenix.Server.Models;
using Calisthenix.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Calisthenix.Server.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly CalisthenixDbContext _context;
        public WorkoutService(CalisthenixDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username)
                   ?? throw new Exception("User not found");
        }

        public async Task<IEnumerable<Workout>> GetAllAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(int id, int userId)
        {
            return await _context.Workouts
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
        }

        public async Task<Workout> CreateAsync(Workout workout, int userId)
        {
            var exists = await _context.Workouts
                .AnyAsync(w => w.Name == workout.Name && w.UserId == userId);

            if (exists)
                throw new Exception("Workout already added.");

            workout.UserId = userId;

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return workout;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (workout == null) return false;

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Workout> GetOrCreateDefaultWorkoutAsync(int userId)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.UserId == userId && w.Name == "MyWorkout");

            if (workout == null)
            {
                workout = new Workout
                {
                    UserId = userId,
                    Name = "MyWorkout"
                };
                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync();
            }

            return workout;
        }

        public async Task<bool> AddExerciseToWorkoutAsync(int workoutId, int exerciseId)
        {
            bool alreadyExists = await _context.WorkoutExercises
                .AnyAsync(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);

            if (alreadyExists)
                return false;

            _context.WorkoutExercises.Add(new WorkoutExercise
            {
                WorkoutId = workoutId,
                ExerciseId = exerciseId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddExerciseToUserWorkoutAsync(int userId, int exerciseId)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (workout == null)
            {
                workout = new Workout
                {
                    Name = "My Workout",
                    UserId = userId
                };

                _context.Workouts.Add(workout);
                await _context.SaveChangesAsync(); // Save so we have the workout.Id
            }

            if (workout.WorkoutExercises.Any(we => we.ExerciseId == exerciseId))
                return;

            workout.WorkoutExercises.Add(new WorkoutExercise
            {
                WorkoutId = workout.Id,
                ExerciseId = exerciseId
            });

            await _context.SaveChangesAsync();
        }
    }
}
