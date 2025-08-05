using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Calisthenix.Server.Data;
using Calisthenix.Server.Services;
using Calisthenix.Server.Models;
using System.Threading.Tasks;
using Calisthenix.Server.Models.DTOs;
using System.Text.Json;

public class WorkoutServiceTests
{
    [Fact]
    public async Task CreateWorkoutAsyncCreatesWorkoutAndReturnsDTO()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutServiceCreateWorkoutDto")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var service = new WorkoutService(context);

        string userId = "1";
        var dto = new CreateWorkoutDTO { Name = "Push Routine" };

        // Act
        var result = await service.CreateWorkoutAsync(userId, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Push Routine", result.Name);

        var workoutInDb = await context.Workouts.FirstOrDefaultAsync(w => w.Id == result.Id);
        Assert.NotNull(workoutInDb);
        Assert.Equal(1, workoutInDb.UserId);
        Assert.Equal("Push Routine", workoutInDb.Name);
    }

    [Fact]
    public async Task GetWorkoutsByUserIdAsyncReturnsUserWorkoutsAsDTOs()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutServiceGetWorkoutsByUserId")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var service = new WorkoutService(context);

        var workouts = new List<Workout>
    {
        new Workout { Id = 1, Name = "Morning Routine", UserId = 1 },
        new Workout { Id = 2, Name = "Evening Pump", UserId = 1 },
        new Workout { Id = 3, Name = "Not My Workout", UserId = 2 }
    };

        context.Workouts.AddRange(workouts);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetWorkoutsByUserIdAsync("1");

        // Assert
        Assert.Equal(2, result.Count);

        var names = result.Select(w => w.Name).ToList();
        Assert.Contains("Morning Routine", names);
        Assert.Contains("Evening Pump", names);
        Assert.DoesNotContain("Not My Workout", names);
    }

    [Fact]
    public async Task AddExerciseToWorkoutAsyncAddsWhenValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutServiceAddExerciseToWorkoutValid")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var workout = new Workout { Id = 1, Name = "Full Body", UserId = 1, WorkoutExercises = new List<WorkoutExercise>() };
        var exercise = new Exercise
        {
            Id = 10,
            Name = "Burpee",
            Description = "Full body",
            Category = "Cardio",
            Difficulty = "Intermediate",
            Equipment = "None",
            VideoUrl = "http://example.com",
            ImageUrl = "http://img.com",
            UserId = 1
        };

        context.Workouts.Add(workout);
        context.Exercises.Add(exercise);
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.AddExerciseToWorkoutAsync(1, 10, "1");

        // Assert
        Assert.True(result);
        var link = await context.WorkoutExercises.FirstOrDefaultAsync(w => w.WorkoutId == 1 && w.ExerciseId == 10);
        Assert.NotNull(link);
    }

    [Fact]
    public async Task AddExerciseToWorkoutAsyncReturnsFalseWhenAlreadyExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutServiceAddExerciseToWorkoutAlreadyExists")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var workout = new Workout
        {
            Id = 1,
            Name = "Cardio",
            UserId = 1,
            WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { WorkoutId = 1, ExerciseId = 20 }
        }
        };

        context.Workouts.Add(workout);
        context.Exercises.Add(new Exercise
        {
            Id = 20,
            Name = "Jumping Jacks",
            Description = "Cardio move",
            Category = "Cardio",
            Difficulty = "Beginner",
            Equipment = "None",
            VideoUrl = "http://video.com",
            ImageUrl = "http://image.com",
            UserId = 1
        }); 
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.AddExerciseToWorkoutAsync(1, 20, "1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddExerciseToWorkoutAsyncReturnsFalseWhenWorkoutNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutServiceAddExerciseToWorkoutNoWorkout")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var service = new WorkoutService(context);

        // Act
        var result = await service.AddExerciseToWorkoutAsync(999, 30, "1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveExerciseFromWorkoutAsync_RemovesLink_WhenValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_RemoveExercise_Valid")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var workout = new Workout
        {
            Id = 1,
            Name = "Leg Day",
            UserId = 1,
            WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { WorkoutId = 1, ExerciseId = 100 }
        }
        };

        context.Workouts.Add(workout);
        context.Exercises.Add(new Exercise
        {
            Id = 100,
            Name = "Lunges",
            Description = "Legs",
            Category = "Legs",
            Difficulty = "Intermediate",
            Equipment = "None",
            VideoUrl = "",
            ImageUrl = "",
            UserId = 1
        });

        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.RemoveExerciseFromWorkoutAsync(1, 100, "1");

        // Assert
        Assert.True(result);
        var stillLinked = context.WorkoutExercises.Any(w => w.ExerciseId == 100 && w.WorkoutId == 1);
        Assert.False(stillLinked);
    }

    [Fact]
    public async Task RemoveExerciseFromWorkoutAsync_ReturnsFalse_WhenWorkoutNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_RemoveExercise_NoWorkout")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var service = new WorkoutService(context);

        // Act
        var result = await service.RemoveExerciseFromWorkoutAsync(999, 100, "1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveExerciseFromWorkoutAsync_ReturnsFalse_WhenExerciseNotInWorkout()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_RemoveExercise_NotInWorkout")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var workout = new Workout
        {
            Id = 1,
            Name = "Upper Body",
            UserId = 1,
            WorkoutExercises = new List<WorkoutExercise>() // empty
        };

        context.Workouts.Add(workout);
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.RemoveExerciseFromWorkoutAsync(1, 500, "1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateWorkoutNameAsync_UpdatesName_WhenWorkoutExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_UpdateWorkoutName_Valid")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var workout = new Workout { Id = 1, Name = "Old Name", UserId = 1 };
        context.Workouts.Add(workout);
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.UpdateWorkoutNameAsync(1, "1", "New Name");

        // Assert
        Assert.True(result);
        var updated = await context.Workouts.FindAsync(1);
        Assert.Equal("New Name", updated.Name);
    }

    [Fact]
    public async Task UpdateWorkoutNameAsync_ReturnsFalse_WhenWorkoutNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_UpdateWorkoutName_NotFound")
            .Options;

        using var context = new CalisthenixDbContext(options);

        context.Workouts.Add(new Workout { Id = 1, Name = "Test", UserId = 1 });
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.UpdateWorkoutNameAsync(1, "999", "ShouldNotUpdate");

        // Assert
        Assert.False(result);
        var stillOriginal = await context.Workouts.FindAsync(1);
        Assert.Equal("Test", stillOriginal.Name);
    }

    [Fact]
    public async Task GetOrCreateDefaultWorkoutAsync_CreatesWorkout_IfNotExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_GetOrCreateWorkout_Create")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var service = new WorkoutService(context);

        // Act
        var result = await service.GetOrCreateDefaultWorkoutAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("MyWorkout", result.Name);
        Assert.Equal(1, result.UserId);

        var inDb = await context.Workouts.FirstOrDefaultAsync(w => w.Name == "MyWorkout" && w.UserId == 1);
        Assert.NotNull(inDb);
    }

    [Fact]
    public async Task GetOrCreateDefaultWorkoutAsync_ReturnsExisting_IfAlreadyExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("WorkoutService_GetOrCreateWorkout_Exists")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var existing = new Workout { Id = 99, Name = "MyWorkout", UserId = 2 };
        context.Workouts.Add(existing);
        await context.SaveChangesAsync();

        var service = new WorkoutService(context);

        // Act
        var result = await service.GetOrCreateDefaultWorkoutAsync(2);

        // Assert
        Assert.Equal(99, result.Id); // same ID
        Assert.Equal("MyWorkout", result.Name);
        Assert.Equal(2, result.UserId);
    }

}
