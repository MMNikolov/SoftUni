using System.Security.Claims;
using System.Text.Json;
using Calisthenix.Server.Controllers;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Calisthenix.Tests.Controllers
{
    public class CommentsControllerTests
    {
        [Fact]
        public async Task GetCommentsReturnsCommentsList()
        {
            var mockService = new Mock<ICommentService>();
            mockService.Setup(s => s.GetCommentsForExerciseAsync(5, null))
                       .ReturnsAsync(new List<CommentDTO> { new() 
                       { 
                           Id = 1, 
                           Content = "Great" 
                       }});

            var controller = new CommentsController(mockService.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity()) 
                }
            };

            var result = await controller.GetComments(5) as OkObjectResult;

            Assert.NotNull(result);
            var comments = Assert.IsAssignableFrom<IEnumerable<CommentDTO>>(result.Value);
            Assert.Single(comments);
        }

        [Fact]
        public async Task PostCommentReturnsCreatedComment()
        {
            var mockService = new Mock<ICommentService>();
            mockService.Setup(s => s.AddCommentAsync(5, 1, "Nice"))
                       .ReturnsAsync(new CommentDTO 
                       { 
                           Id = 1, 
                           Content = "Nice" 
                       });

            var controller = new CommentsController(mockService.Object);
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

            var dto = new CreateCommentDTO { Content = "Nice" };

            var result = await controller.PostComment(5, dto) as OkObjectResult;

            Assert.NotNull(result);
            var comment = Assert.IsType<CommentDTO>(result.Value);
            Assert.Equal("Nice", comment.Content);
        }

        [Fact]
        public async Task ReactToCommentReturnsOkWhenSuccess()
        {
            var mockService = new Mock<ICommentService>();
            mockService.Setup(s => s.ToggleReactionAsync(1, 1)).ReturnsAsync(true);

            var controller = new CommentsController(mockService.Object);

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

            var dto = new CommentReactionDTO { CommentId = 1 };

            var result = await controller.ReactToComment(dto) as OkObjectResult;

            Assert.NotNull(result);
            var json = JsonSerializer.Serialize(result.Value);
            using var doc = JsonDocument.Parse(json);
            Assert.Equal("Thumbs up added.", doc.RootElement.GetProperty("message").GetString());
        }

        [Fact]
        public async Task ReactToCommentReturnsBadRequestWhenAlreadyReacted()
        {
            var mockService = new Mock<ICommentService>();
            mockService.Setup(s => s.ToggleReactionAsync(1, 1)).ReturnsAsync(false);

            var controller = new CommentsController(mockService.Object);
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

            var dto = new CommentReactionDTO { CommentId = 1 };

            var result = await controller.ReactToComment(dto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Already reacted to this comment.", result.Value);
        }

        [Fact]
        public async Task ToggleLikeReturnsLikedFlag()
        {
            var mockService = new Mock<ICommentService>();
            mockService.Setup(s => s.ToggleReactionAsync(1, 1)).ReturnsAsync(true);

            var controller = new CommentsController(mockService.Object);
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

            var result = await controller.ToggleLike(1) as OkObjectResult;

            Assert.NotNull(result);
            var json = JsonSerializer.Serialize(result.Value);
            using var doc = JsonDocument.Parse(json);
            Assert.True(doc.RootElement.GetProperty("liked").GetBoolean());
        }

    }
}
