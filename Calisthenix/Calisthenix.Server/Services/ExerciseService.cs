using Calisthenix.Server.Models;
using Calisthenix.Server.Data;
using Microsoft.EntityFrameworkCore;
using Calisthenix.Server.Services.Interfaces;
using Calisthenix.Server.Models.DTOs;

public class ExerciseService : IExerciseService
{
    private readonly CalisthenixDbContext _context;

    public ExerciseService(CalisthenixDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExerciseDTO>> GetAllExercisesAsync()
    {
        return await _context.Exercises
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
        
    }

    public async Task DeleteExerciseAsync(string id)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e => e.Id.ToString().ToLower() == id.ToString().ToLower());

        if (exercise == null)
        {
            throw new InvalidOperationException("Exercise not found");
        }

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();

    }
}
