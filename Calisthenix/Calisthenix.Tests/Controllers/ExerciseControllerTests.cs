using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Calisthenix.Server.Controllers;
using Calisthenix.Server.Models;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Calisthenix.Tests.Controllers
{
    public class ExerciseControllerTests
    {
        [Fact]
        public async Task GetAllExercises_ReturnsPagedResults()
        {
            var mockService = new Mock<IExerciseService>();
            mockService.Setup(s => s.GetPaginatedExercisesAsync(1, 10))
                       .ReturnsAsync(new List<ExerciseDTO>
                       {
                   new ExerciseDTO { Id = 1, Name = "Push Up" },
                   new ExerciseDTO { Id = 2, Name = "Pull Up" }
                       });

            var controller = new ExerciseController(mockService.Object);

            var result = await controller.GetAllExercises(1, 10) as OkObjectResult;

            Assert.NotNull(result);
            var list = Assert.IsAssignableFrom<IEnumerable<ExerciseDTO>>(result.Value);
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task GetAllExercises_ReturnsBadRequest_WhenPageInvalid()
        {
            var mockService = new Mock<IExerciseService>();
            var controller = new ExerciseController(mockService.Object);

            var result = await controller.GetAllExercises(0, 10) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Page and pageSize must be greater than 0.", result.Value);
        }

        [Fact]
        public async Task GetExerciseById_ReturnsExercise_WhenFound()
        {
            var mockService = new Mock<IExerciseService>();
            mockService.Setup(s => s.GetExerciseByIdAsync("1"))
                       .ReturnsAsync(new Exercise { Id = 1, Name = "Push Up" });

            var controller = new ExerciseController(mockService.Object);
            var result = await controller.GetExerciseById("1") as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetExerciseById_ReturnsNotFound_WhenMissing()
        {
            var mockService = new Mock<IExerciseService>();
            mockService.Setup(s => s.GetExerciseByIdAsync("999")).ReturnsAsync((Exercise)null);

            var controller = new ExerciseController(mockService.Object);
            var result = await controller.GetExerciseById("999") as NotFoundResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddExercise_ReturnsCreated_WhenValid()
        {
            var mockService = new Mock<IExerciseService>();
            mockService.Setup(s => s.AddExerciseAsync(It.IsAny<Exercise>()))
                       .Returns(Task.CompletedTask);

            var controller = new ExerciseController(mockService.Object);

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
                }
            };

            var exercise = new Exercise
            {
                Name = "Plank",
                Description = "Hold still",
                Category = "Core",
                Equipment = "None",
                Difficulty = "Beginner"
            };

            var result = await controller.AddExercise(exercise) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal("GetExerciseById", result.ActionName);
        }

        [Fact]
        public async Task AddExercise_ReturnsBadRequest_WhenNameMissing()
        {
            var mockService = new Mock<IExerciseService>();
            var controller = new ExerciseController(mockService.Object);

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
                }
            };

            var exercise = new Exercise { Name = "", Description = "" };

            var result = await controller.AddExercise(exercise) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Exercise name and description are required.", result.Value);
        }

        [Fact]
        public async Task DeleteExercise_ReturnsNoContent_WhenDeleted()
        {
            var mockService = new Mock<IExerciseService>();
            mockService.Setup(s => s.DeleteExerciseAsync("1", 1)).ReturnsAsync(true);

            var controller = new ExerciseController(mockService.Object);

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
                }
            };

            var result = await controller.DeleteExercise("1") as NoContentResult;
            Assert.NotNull(result);
        }

    }
}
