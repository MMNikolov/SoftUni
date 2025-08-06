using Calisthenix.Server.Data;
using Calisthenix.Server.Services;
using Calisthenix.Server.Models;
using Microsoft.EntityFrameworkCore;

public class CommentServiceTests
{
    [Fact]
    public async Task AddCommentAsyncShouldReturnCommentDTO()
    {
        // Arrange  
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        using var context = new CalisthenixDbContext(options);

        context.Users.Add(new User
        {
            Id = 1,
            Username = "Pesho",
            PasswordHash = "dummyhash"
        });
        await context.SaveChangesAsync();

        var service = new CommentService(context);

        // Act  
        var result = await service.AddCommentAsync(5, 1, "Nice exercise!");

        // Assert  
        Assert.NotNull(result);
        Assert.Equal("Nice exercise!", result.Content);
        Assert.Equal("Pesho", result.Username);
    }

    [Fact]
    public async Task GetCommentsForExerciseAsyncReturnsCorrectData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDBGetComments")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user1 = new User 
        { 
            Id = 1, 
            Username = "Gosho", 
            PasswordHash = "hash1" 
        };
        var user2 = new User 
        { 
            Id = 2, 
            Username = "Pesho", 
            PasswordHash = "hash2" 
        };

        var comment1 = new Comment 
        { 
            Id = 1, 
            UserId = 1, 
            ExerciseId = 99, 
            Content = "Great!", 
            CreatedAt = DateTime.UtcNow 
        };
        var comment2 = new Comment 
        { 
            Id = 2, 
            UserId = 1, 
            ExerciseId = 99, 
            Content = "Challenging!", 
            CreatedAt = DateTime.UtcNow 
        };

        var reaction = new CommentReaction 
        { 
            Id = 1, 
            UserId = 2, 
            CommentId = 1, 
            IsThumbsUp = true 
        };

        context.Users.AddRange(user1, user2);
        context.Comments.AddRange(comment1, comment2);
        context.CommentReactions.Add(reaction);
        await context.SaveChangesAsync();

        var service = new CommentService(context);

        // Act
        var results = await service.GetCommentsForExerciseAsync(99, 2);

        // Assert
        Assert.Equal(2, results.Count);

        var first = results.First(c => c.Id == 1);
        var second = results.First(c => c.Id == 2);

        Assert.Equal("Great!", first.Content);
        Assert.Equal("Challenging!", second.Content);

        Assert.Equal(1, first.ThumbsUpCount);
        Assert.True(first.LikedByCurrentUser);

        Assert.Equal(0, second.ThumbsUpCount);
        Assert.False(second.LikedByCurrentUser);
    }

    [Fact]
    public async Task ToggleReactionAsyncShouldAddReactionWhenUserHasNotReactedBefore()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDBToggleReactionAdd")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user = new User 
        { 
            Id = 1, 
            Username = "Vladimir4o", 
            PasswordHash = "hash" 
        };
        var comment = new Comment 
        { 
            Id = 1, 
            UserId = 2, 
            ExerciseId = 88, 
            Content = "Try it!" 
        };

        context.Users.Add(user);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var service = new CommentService(context);

        // Act
        var result = await service.ToggleReactionAsync(commentId: 1, userId: 1);

        // Assert
        Assert.True(result);

        var reaction = await context.CommentReactions
            .FirstOrDefaultAsync(r => r.CommentId == 1 && r.UserId == 1);

        Assert.NotNull(reaction);
        Assert.True(reaction.IsThumbsUp);
    }

    [Fact]
    public async Task ToggleReactionAsyncShouldReturnFalseWhenUserAlreadyReacted()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CalisthenixDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDBToggleReactionAlreadyLiked")
            .Options;

        using var context = new CalisthenixDbContext(options);

        var user = new User 
        { 
            Id = 1, 
            Username = "TestUser", 
            PasswordHash = "hash" 
        };
        var comment = new Comment 
        { 
            Id = 1, 
            UserId = 2, 
            ExerciseId = 88, 
            Content = "Nice!" 
        };
        var existingReaction = new CommentReaction
        {
            Id = 1,
            UserId = 1,
            CommentId = 1,
            IsThumbsUp = true
        };

        context.Users.Add(user);
        context.Comments.Add(comment);
        context.CommentReactions.Add(existingReaction);
        await context.SaveChangesAsync();

        var service = new CommentService(context);

        // Act
        var result = await service.ToggleReactionAsync(commentId: 1, userId: 1);

        // Assert
        Assert.False(result);

        var count = await context.CommentReactions
            .CountAsync(r => r.CommentId == 1 && r.UserId == 1);

        Assert.Equal(1, count);
    }
}
