namespace Calisthenix.Server.Services.Interfaces
{
    using Calisthenix.Server.Models;
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsForExerciseAsync(int exerciseId);
        Task<Comment> AddCommentAsync(int exerciseId, int userId, string content);
        Task<bool> ToggleReactionAsync(int commentId, int userId);
    }
}
