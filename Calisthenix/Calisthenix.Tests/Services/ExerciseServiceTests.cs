using Moq;
using Microsoft.Extensions.Caching.Memory;
using Calisthenix.Server.Data;
using Calisthenix.Server.Models;
using Microsoft.EntityFrameworkCore;

public class ExerciseServiceTests
{
    [Fact]
    public async Task GetAllExercisesAsyncReturnsFilteredExercisesAndCachesResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseService_GetAll")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user = new User { Id = 1, Username = "user1", PasswordHash = "hash" };

        var exercises = new List<Exercise>
        {
            new Exercise
            {
                Id = 1,
                Name = "Push Up",
                Description = "Push the ground",
                Category = "Strength",
                Equipment = "None",
                Difficulty = "Beginner",
                VideoUrl = "http://example.com",
                ImageUrl = "http://image.com",
                UserId = user.Id,
                User = user
            },
            new Exercise
            {
                Id = 2,
                Name = "",
                Description = "Missing name",
                Category = "Strength",
                Equipment = "None",
                Difficulty = "Beginner",
                VideoUrl = "http://example.com",
                ImageUrl = "http://image.com",
                UserId = user.Id,
                User = user
            },
            new Exercise
            {
                Id = 3,
                Name = "Squat",
                Description = "",
                Category = "Strength",
                Equipment = "None",
                Difficulty = "Beginner",
                VideoUrl = "http://example.com",
                ImageUrl = "http://image.com",
                UserId = user.Id,
                User = user
            },
            new Exercise
            {
                Id = 4,
                Name = "Pull Up",
                Description = "Use bar",
                Category = "Strength",
                Equipment = "None",
                Difficulty = "Beginner",
                VideoUrl = "http://example.com",
                ImageUrl = "http://image.com",
                UserId = user.Id,
                User = user
            }
        };

        context.Exercises.AddRange(exercises);
        await context.SaveChangesAsync();

        var cacheMock = new Mock<IMemoryCache>();
        var cacheEntryMock = new Mock<ICacheEntry>();

        object dummy = null;

        cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out dummy))
                 .Returns(false);

        cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>()))
                 .Returns(cacheEntryMock.Object);

        var service = new ExerciseService(context, cacheMock.Object);

        // Act
        var result = await service.GetAllExercisesAsync();
        var list = result.ToList();

        // Assert
        Assert.Equal(2, list.Count); // Only Push Up and Pull Up are valid
        Assert.Contains(list, e => e.Name == "Push Up");
        Assert.Contains(list, e => e.Name == "Pull Up");
    }

    [Fact]
    public async Task GetExerciseByIdAsyncReturnsExerciseWhenFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseServiceGetByIdValid")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var exercise = new Exercise
        {
            Id = 101,
            Name = "Handstand",
            Description = "Balance on hands",
            Category = "Balance",
            Equipment = "None",
            Difficulty = "Advanced",
            VideoUrl = "http://video.com",
            ImageUrl = "http://img.com",
            UserId = 1,
            User = new User { Id = 1, Username = "tester", PasswordHash = "hash" }
        };

        context.Exercises.Add(exercise);
        await context.SaveChangesAsync();

        var memoryCache = new Mock<IMemoryCache>();
        var service = new ExerciseService(context, memoryCache.Object);

        // Act
        var result = await service.GetExerciseByIdAsync("101");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Handstand", result.Name);
        Assert.Equal("Balance on hands", result.Description);
    }

    [Fact]
    public async Task GetExerciseByIdAsyncThrowsWhenNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseService_GetById_Invalid")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var memoryCache = new Mock<IMemoryCache>();
        var service = new ExerciseService(context, memoryCache.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.GetExerciseByIdAsync("999"));

        Assert.Equal("Exercise not found", ex.Message);
    }

    [Fact]
    public async Task AddExerciseAsyncAddsExerciseAndClearsCache()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseServiceAddExercise")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var cacheMock = new Mock<IMemoryCache>();

        var service = new ExerciseService(context, cacheMock.Object);

        var exercise = new Exercise
        {
            Name = "L-Sit",
            Description = "Core and balance move",
            Category = "Core",
            Equipment = "Parallettes",
            Difficulty = "Intermediate",
            VideoUrl = "http://video.com",
            ImageUrl = "http://image.com",
            UserId = 1
        };

        // Act
        await service.AddExerciseAsync(exercise);

        // Assert
        var saved = await context.Exercises.FirstOrDefaultAsync(e => e.Name == "L-Sit");
        Assert.NotNull(saved);
        Assert.Equal("L-Sit", saved.Name);
        Assert.Equal("Parallettes", saved.Equipment);

        cacheMock.Verify(c => c.Remove("AllExercises"), Times.Once);
    }

    [Fact]
    public async Task DeleteExerciseAsyncDeletesExerciseAndWorkoutLinksWhenFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseServiceDeleteExerciseFound")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var exercise = new Exercise
        {
            Id = 1,
            Name = "Plank",
            Description = "Hold still",
            Category = "Core",
            Equipment = "None",
            Difficulty = "Beginner",
            VideoUrl = "",
            ImageUrl = "",
            UserId = 1,
            WorkoutExercises = new List<WorkoutExercise>
            {
                new WorkoutExercise 
                { 
                    WorkoutId = 100, 
                    ExerciseId = 1 
                }
            }
        };

        context.Exercises.Add(exercise);
        await context.SaveChangesAsync();

        var cacheMock = new Mock<IMemoryCache>();
        var service = new ExerciseService(context, cacheMock.Object);

        // Act
        var result = await service.DeleteExerciseAsync("1", 1);

        // Assert
        Assert.True(result);
        Assert.Null(await context.Exercises.FindAsync(1));
        Assert.Empty(context.WorkoutExercises); 

        cacheMock.Verify(c => c.Remove("AllExercises"), Times.Once);
    }

    [Fact]
    public async Task DeleteExerciseAsyncReturnsFalseWhenExerciseNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("ExerciseServiceDeleteExerciseNotFound")
            .Options;

        using var context = new CalisthenixDbContext(options);
        var cacheMock = new Mock<IMemoryCache>();
        var service = new ExerciseService(context, cacheMock.Object);

        // Act
        var result = await service.DeleteExerciseAsync("99", 1);

        // Assert
        Assert.False(result);
        cacheMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task UpdateExerciseAsyncUpdatesExistingExercise()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("UpdateExerciseTest")
            .Options;

        var cacheMock = new Mock<IMemoryCache>();

        using var context = new CalisthenixDbContext(options);

        var existingExercise = new Exercise
        {
            Id = 1,
            Name = "Old Name",
            Description = "Old Description",
            Category = "Old Cat",
            Equipment = "Old Equip",
            Difficulty = "Beginner",
            VideoUrl = "oldurl",
            ImageUrl = "oldimg",
            UserId = 1
        };

        context.Exercises.Add(existingExercise);
        await context.SaveChangesAsync();

        var service = new ExerciseService(context, cacheMock.Object);

        var updatedExercise = new Exercise
        {
            Name = "New Name",
            Description = "New Description",
            Category = "New Cat",
            Equipment = "New Equip",
            Difficulty = "Advanced",
            VideoUrl = "newurl",
            ImageUrl = "newimg"
        };

        // Act
        await service.UpdateExerciseAsync(existingExercise.Id.ToString(), updatedExercise);

        // Assert
        var result = await context.Exercises.FindAsync(existingExercise.Id);
        Assert.Equal("New Name", result.Name);
        Assert.Equal("Advanced", result.Difficulty);
        Assert.Equal("newurl", result.VideoUrl);
    }

    [Fact]
    public async Task UpdateExerciseAsyncThrowsExceptionWhenExerciseNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("UpdateExerciseNotFound")
            .Options;

        var cacheMock = new Mock<IMemoryCache>();
        using var context = new CalisthenixDbContext(options);

        var service = new ExerciseService(context, cacheMock.Object);

        var updatedExercise = new Exercise
        {
            Name = "Doesn't Matter",
            Description = "Whatever"
        };

        // Act + Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.UpdateExerciseAsync(Guid.NewGuid().ToString(), updatedExercise));
    }

}