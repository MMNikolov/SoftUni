using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Calisthenix.Server.Data;
using Calisthenix.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using Calisthenix.Server.Enums;
using Calisthenix.Server.Models.DTOs;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsyncCreatesUserAndReturnsToken()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthService_Register")
            .Options;

        var jwtKey = "supersecretkey!123supersecretkey!123";
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns(jwtKey);

        using var context = new CalisthenixDbContext(options);
        var service = new AuthService(context, configMock.Object);

        string username = "Georgi";
        string password = "123";

        // Act
        var token = await service.RegisterAsync(username, password);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));

        var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        Assert.NotNull(userInDb);
        Assert.NotEqual(password, userInDb.PasswordHash); 

        var handler = new JwtSecurityTokenHandler();
        var readable = handler.CanReadToken(token);
        Assert.True(readable);
    }

    [Fact]
    public async Task RegisterAsyncThrowsExceptionWhenUserAlreadyExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceRegisterAlreadyExists")
            .Options;

        var jwtKey = "anothersecretkeysuperlong123456";
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns(jwtKey);

        using var context = new CalisthenixDbContext(options);

        context.Users.Add(new Calisthenix.Server.Models.User
        {
            Username = "Alexcho",
            PasswordHash = "hashed"
        });
        await context.SaveChangesAsync();

        var service = new AuthService(context, configMock.Object);

        // Act + Assert
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            service.RegisterAsync("Alexcho", "anyPassword"));

        Assert.Equal("User already exists", ex.Message);
    }

    [Fact]
    public async Task LoginAsyncReturnsTokenWhenCredentialsAreValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceLoginValid")
            .Options;

        var jwtKey = "validloginsecretkeymustbelong12345";
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns(jwtKey);

        using var context = new CalisthenixDbContext(options);

        string username = "Mitacheto";
        string password = "123";
        string hashed = BCrypt.Net.BCrypt.HashPassword(password);

        context.Users.Add(new Calisthenix.Server.Models.User
        {
            Username = username,
            PasswordHash = hashed
        });
        await context.SaveChangesAsync();

        var service = new AuthService(context, configMock.Object);

        // Act
        var token = await service.LoginAsync(username, password);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Theory]
    [InlineData("wronguser", "mypassword")]    
    [InlineData("realuser", "wrongpassword")]  
    public async Task LoginAsyncThrowsExceptionWhenCredentialsInvalid(string username, string password)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceLoginInvalid")
            .Options;

        var jwtKey = "invalidsecretkeysupersecurekey";
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["Jwt:Key"]).Returns(jwtKey);

        using var context = new CalisthenixDbContext(options);

        context.Users.Add(new Calisthenix.Server.Models.User
        {
            Username = "realuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctpassword")
        });
        await context.SaveChangesAsync();

        var service = new AuthService(context, configMock.Object);

        // Act + Assert
        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.LoginAsync(username, password));
        Assert.Equal("Invalid username or password", ex.Message);
    }

    [Fact]
    public async Task ChangePasswordAsyncUpdatesPasswordWhenCurrentPasswordIsCorrect()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceChangePasswordSuccess")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user = new Calisthenix.Server.Models.User
        {
            Id = 1,
            Username = "Pesho",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("oldpass")
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var configMock = new Mock<IConfiguration>();
        var service = new AuthService(context, configMock.Object);

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "oldpass",
            NewPassword = "newpass123"
        };

        // Act
        var result = await service.ChangePasswordAsync(1, request);

        // Assert
        Assert.Equal(AuthResult.Success, result);

        var updatedUser = await context.Users.FindAsync(1);
        Assert.True(BCrypt.Net.BCrypt.Verify("newpass123", updatedUser.PasswordHash));
    }

    [Fact]
    public async Task ChangePasswordAsyncReturnsInvalidPasswordWhenOldPasswordIncorrect()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceChangePasswordInvalidOld")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user = new Calisthenix.Server.Models.User
        {
            Id = 2,
            Username = "wrongpassuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctold")
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var configMock = new Mock<IConfiguration>();
        var service = new AuthService(context, configMock.Object);

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "wrongold",
            NewPassword = "newpass"
        };

        // Act
        var result = await service.ChangePasswordAsync(2, request);

        // Assert
        Assert.Equal(AuthResult.InvalidPassword, result);
    }

    [Fact]
    public async Task ChangePasswordAsyncReturnsNotFoundWhenUserDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase("AuthServiceChangePasswordNotFound")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var configMock = new Mock<IConfiguration>();
        var service = new AuthService(context, configMock.Object);

        var request = new ChangePasswordRequest
        {
            CurrentPassword = "anything",
            NewPassword = "newpass"
        };

        // Act
        var result = await service.ChangePasswordAsync(999, request);

        // Assert
        Assert.Equal(AuthResult.NotFound, result);
    }
}
