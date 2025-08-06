using Xunit;
using Moq;
using System.Threading.Tasks;
using Calisthenix.Server.Controllers;
using Calisthenix.Server.Services.Interfaces;
using Calisthenix.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Calisthenix.Server.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class AuthControllerTests
{
    [Fact]
    public async Task RegisterReturnsOkWithToken()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(s => s.RegisterAsync("Pesho", "123"))
                       .ReturnsAsync("fake-jwt-token");

        var controller = new AuthController(mockAuthService.Object);

        var dto = new LoginDTO
        {
            Username = "Pesho",
            Password = "123"
        };

        // Act
        var result = await controller.Register(dto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        // Deserialize anonymous object to JSON
        var json = JsonSerializer.Serialize(result.Value);
        using var doc = JsonDocument.Parse(json);
        var token = doc.RootElement.GetProperty("token").GetString();

        Assert.Equal("fake-jwt-token", token);
    }

    [Fact]
    public async Task LoginReturnsOkWithToken()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(s => s.LoginAsync("Pesho", "123"))
                       .ReturnsAsync("jwt-login-token");

        var controller = new AuthController(mockAuthService.Object);

        var dto = new LoginDTO
        {
            Username = "Pesho",
            Password = "123"
        };

        // Act
        var result = await controller.Login(dto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        // Parse the anonymous object
        var json = JsonSerializer.Serialize(result.Value);
        using var doc = JsonDocument.Parse(json);
        var token = doc.RootElement.GetProperty("token").GetString();

        Assert.Equal("jwt-login-token", token);
    }

    [Fact]
    public async Task ChangePasswordReturnsOkWhenSuccessful()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(s => s.ChangePasswordAsync(1, It.IsAny<ChangePasswordRequest>()))
                       .ReturnsAsync(AuthResult.Success);

        var controller = new AuthController(mockAuthService.Object);

        // Mock User.Identity with NameIdentifier = "1"
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "mock");
        var user = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "123",
            NewPassword = "1234"
        };

        // Act
        var result = await controller.ChangePassword(request) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Password changed successfully.", result.Value);
    }

    [Fact]
    public async Task ChangePasswordReturnsNotFoundWhenUserNotFound()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(s => s.ChangePasswordAsync(1, It.IsAny<ChangePasswordRequest>()))
                       .ReturnsAsync(AuthResult.NotFound);

        var controller = new AuthController(mockAuthService.Object);

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "mock");
        var user = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "123",
            NewPassword = "1234"
        };

        // Act
        var result = await controller.ChangePassword(request) as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal("User not found.", result.Value);
    }

    [Fact]
    public async Task ChangePasswordReturnsBadRequestWhenOldPasswordWrong()
    {
        // Arrange
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(s => s.ChangePasswordAsync(1, It.IsAny<ChangePasswordRequest>()))
                       .ReturnsAsync(AuthResult.InvalidPassword);

        var controller = new AuthController(mockAuthService.Object);

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "mock");
        var user = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "123",
            NewPassword = "1234"
        };

        // Act
        var result = await controller.ChangePassword(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Current password is incorrect.", result.Value);
    }
}
