using Calisthenix.Server.Models;
using Calisthenix.Server.Data;
using Microsoft.EntityFrameworkCore;
using Calisthenix.Server.Services.Interfaces;
using Calisthenix.Server.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;

public class ExerciseService : IExerciseService
{
    private readonly CalisthenixDbContext _context;
    private readonly IMemoryCache _cache;

    private const string AllExercisesCacheKey = "AllExercises";

    public ExerciseService(CalisthenixDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<ExerciseDTO>> GetAllExercisesAsync()
    {
        if (_cache.TryGetValue(AllExercisesCacheKey, out IEnumerable<ExerciseDTO> cachedExercises))
        {
            return cachedExercises;
        }

        var exercises =  await _context.Exercises
            .Where(e => !string.IsNullOrEmpty(e.Name) && !string.IsNullOrEmpty(e.Description))
            .Include(e => e.User)
            .AsNoTracking()
            .Select(e => new ExerciseDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Category = e.Category,
                Equipment = e.Equipment,
                Difficulty = e.Difficulty,
                VideoUrl = e.VideoUrl,
                ImageUrl = e.ImageUrl,
                UserName = e.User.Username
            })
            .ToListAsync();

        _cache.Set(AllExercisesCacheKey, exercises, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        });

        return exercises;
    }

    public async Task<Exercise?> GetExerciseByIdAsync(string id)
    {
        var exercise = await _context.Exercises
            .Where(e => e.Id.ToString() == id)
            .Select(e => new Exercise
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Category = e.Category,
                Equipment = e.Equipment,
                Difficulty = e.Difficulty,
                VideoUrl = e.VideoUrl,
                ImageUrl = e.ImageUrl,
                UserId = e.UserId
            })
            .FirstOrDefaultAsync();

        return exercise ?? throw new InvalidOperationException("Exercise not found");
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        var newExercise = new Exercise
        {
            Name = exercise.Name,
            Description = exercise.Description,
            Category = exercise.Category,
            Equipment = exercise.Equipment,
            Difficulty = exercise.Difficulty,
            VideoUrl = exercise.VideoUrl,
            ImageUrl = exercise.ImageUrl,
            UserId = exercise.UserId
        };

        await _context.Exercises.AddAsync(newExercise);
        await _context.SaveChangesAsync();

        _cache.Remove(AllExercisesCacheKey);
    }

    public async Task UpdateExerciseAsync(string id, Exercise exercise)
    {
        var existing = 
            await _context.Exercises
            .FirstOrDefaultAsync(e => e.Id.ToString() == id);

        if (existing == null) 
        {
            throw new InvalidOperationException("Exercise not found");
        }

        existing.Name = exercise.Name;
        existing.Description = exercise.Description;
        existing.Category = exercise.Category;
        existing.Equipment = exercise.Equipment;
        existing.Difficulty = exercise.Difficulty;
        existing.VideoUrl = exercise.VideoUrl;
        existing.ImageUrl = exercise.ImageUrl;

        _context.Exercises.Update(existing);
        await _context.SaveChangesAsync();

        _cache.Remove(AllExercisesCacheKey);

    }

    public async Task<bool> DeleteExerciseAsync(string id, int userId)
    {
        var exercise = await _context.Exercises
        .Include(e => e.WorkoutExercises)
        .FirstOrDefaultAsync(e => e.Id.ToString() == id && e.UserId == userId);

        if (exercise == null)
            return false;

        _context.WorkoutExercises.RemoveRange(exercise.WorkoutExercises);
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();

        _cache.Remove(AllExercisesCacheKey);
        return true;
    }
}
