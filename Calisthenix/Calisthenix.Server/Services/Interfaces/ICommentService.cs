namespace Calisthenix.Server.Services.Interfaces
{
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Models.DTOs;

    public interface ICommentService
    {
        Task<List<CommentDTO>> GetCommentsForExerciseAsync(int exerciseId, int? currentUserId);
        Task<CommentDTO> AddCommentAsync(int exerciseId, int userId, string content);
        Task<bool> ToggleReactionAsync(int commentId, int userId);
    }
}
