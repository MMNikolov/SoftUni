using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Calisthenix.Server.Controllers;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Services.Interfaces;
using Calisthenix.Server.Models;
using System.Text.Json;

public class WorkoutControllerTests
{
    [Fact]
    public async Task CreateWorkout_ReturnsCreatedWorkout()
    {
        // Arrange
        var mockService = new Mock<IWorkoutService>();
        var expectedWorkout = new WorkoutDTO 
        { 
            Id = 1, 
            Name = "Push Day" 
        };

        mockService.Setup(s => s.CreateWorkoutAsync("1", It.IsAny<CreateWorkoutDTO>()))
                   .ReturnsAsync(expectedWorkout);

        var controller = new WorkoutController(mockService.Object, null);

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "mock");

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(identity)
            }
        };

        var dto = new CreateWorkoutDTO { Name = "Push Day" };

        // Act
        var result = await controller.CreateWorkout(dto) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("GetMyWorkouts", result.ActionName);
        Assert.Equal(expectedWorkout.Id, ((WorkoutDTO)result.Value).Id);
        Assert.Equal("Push Day", ((WorkoutDTO)result.Value).Name);
    }

    [Fact]
    public async Task GetMyWorkoutsReturnsWorkoutsForUser()
    {
        // Arrange
        var mockService = new Mock<IWorkoutService>();
        var workouts = new List<WorkoutDTO>
        {
            new WorkoutDTO 
            { 
                Id = 1,
                Name = "Legs" 
            },
            new WorkoutDTO 
            { 
                Id = 2, 
                Name = "Push Day" 
            }
        };

        mockService.Setup(s => s.GetWorkoutsByUserIdAsync("1"))
                   .ReturnsAsync(workouts);

        var controller = new WorkoutController(mockService.Object, null);

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "mock");

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(identity)
            }
        };

        // Act
        var result = await controller.GetMyWorkouts() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var returnedWorkouts = Assert.IsAssignableFrom<IEnumerable<WorkoutDTO>>(result.Value);
        Assert.Equal(2, returnedWorkouts.Count());
        Assert.Contains(returnedWorkouts, w => w.Name == "Legs");
    }

    [Fact]
    public async Task GetByIdReturnsWorkoutWhenFound()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.GetByIdAsync(1, 1))
                   .ReturnsAsync(new Workout { Id = 1, Name = "Workout A", UserId = 1 });

        var controller = new WorkoutController(mockService.Object, null);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.GetById(1) as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundWhenWorkoutMissing()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.GetByIdAsync(5, 1)).ReturnsAsync((Workout)null);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.GetById(5) as NotFoundObjectResult;
        Assert.NotNull(result);
        Assert.Equal("Workout not found!", result.Value);
    }

    [Fact]
    public async Task AddToWorkoutReturnsOk()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.AddExerciseToUserWorkoutAsync(1, 100))
                   .Returns(Task.CompletedTask);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.AddToWorkout(100) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var json = JsonSerializer.Serialize(result.Value);
        using var doc = JsonDocument.Parse(json);
        var message = doc.RootElement.GetProperty("message").GetString();

        Assert.Equal("Exercise added to workout.", message);
    }

    [Fact]
    public async Task GetAllReturnsRawWorkouts()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.GetAllWorkoutsWithExercisesRawAsync(1))
                   .ReturnsAsync(new[] { new { Id = 1, Name = "W1", WorkoutExercises = new object[0] } });

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.GetAll() as OkObjectResult;
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task AddExerciseToWorkoutReturnsOkWhenSuccess()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.AddExerciseToWorkoutAsync(1, 10, "1")).ReturnsAsync(true);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.AddExerciseToWorkout(1, 10) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task AddExerciseToWorkoutReturnsBadRequestWhenDuplicate()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.AddExerciseToWorkoutAsync(1, 10, "1")).ReturnsAsync(false);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.AddExerciseToWorkout(1, 10) as BadRequestObjectResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task RemoveExerciseReturnsNoContentWhenSuccess()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.RemoveExerciseFromWorkoutAsync(1, 10, "1")).ReturnsAsync(true);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.RemoveExercise(1, 10) as NoContentResult;
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RemoveExerciseReturnsBadRequestWhenNotFound()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.RemoveExerciseFromWorkoutAsync(1, 10, "1")).ReturnsAsync(false);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.RemoveExercise(1, 10) as BadRequestObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task UpdateWorkoutNameReturnsOkWhenUpdated()
    {
        var mockService = new Mock<IWorkoutService>();

        mockService.Setup(s => s.UpdateWorkoutNameAsync(1, "1", "New Name")).ReturnsAsync(true);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var dto = new WorkoutDTO 
        { 
            Name = "New Name" 
        };

        var result = await controller.UpdateWorkoutName(1, dto) as OkObjectResult;

        Assert.NotNull(result);

        var json = JsonSerializer.Serialize(result.Value);
        using var doc = JsonDocument.Parse(json);

        Assert.Equal("Workout name updated.", doc.RootElement.GetProperty("message").GetString());
    }

    [Fact]
    public async Task UpdateWorkoutNameReturnsNotFoundWhenWorkoutMissing()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.UpdateWorkoutNameAsync(1, "1", "New Name")).ReturnsAsync(false);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var dto = new WorkoutDTO 
        { 
            Name = "New Name" 
        };

        var result = await controller.UpdateWorkoutName(1, dto) as NotFoundObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Workout not found or unauthorized.", result.Value);
    }

    [Fact]
    public async Task DeleteWorkoutReturnsNoContentWhenDeleted()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.DeleteAsync(1, 1)).ReturnsAsync(true);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.DeleteWorkout(1) as NoContentResult;
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteWorkoutReturnsNotFoundWhenWorkoutMissing()
    {
        var mockService = new Mock<IWorkoutService>();
        mockService.Setup(s => s.DeleteAsync(1, 1)).ReturnsAsync(false);

        var controller = new WorkoutController(mockService.Object, null);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"))
            }
        };

        var result = await controller.DeleteWorkout(1) as NotFoundObjectResult;

        Assert.NotNull(result);
        Assert.Equal("Workout not found or unauthorized.", result.Value);
    }

}
