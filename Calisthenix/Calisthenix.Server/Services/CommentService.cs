using Calisthenix.Server.Data;
using Calisthenix.Server.Models;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Calisthenix.Server.Services
{
    public class CommentService : ICommentService
    {
        private readonly CalisthenixDbContext _context;
        public CommentService(CalisthenixDbContext context)
        {
            _context = context;
        }

        public async Task<CommentDTO> AddCommentAsync(int exerciseId, int userId, string content)
        {
            var comment = new Comment
            {
                ExerciseId = exerciseId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(userId);

            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                Username = user?.Username ?? "You",
                CreatedAt = comment.CreatedAt,
                ThumbsUpCount = 0,
                LikedByCurrentUser = false
            };
        }

        public async Task<List<CommentDTO>> GetCommentsForExerciseAsync(int exerciseId, int? currentUserId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Reactions)
                .Where(c => c.ExerciseId == exerciseId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Content = c.Content,
                Username = c.User?.Username ?? "Anonymous",
                CreatedAt = c.CreatedAt,
                ThumbsUpCount = c.Reactions.Count(r => r.IsThumbsUp),
                LikedByCurrentUser = currentUserId.HasValue && c.Reactions.Any(r => r.UserId == currentUserId.Value)
            }).ToList();
        }

        public async Task<bool> ToggleReactionAsync(int commentId, int userId)
        {
            var exists = await _context.CommentReactions
                .AnyAsync(r => r.CommentId == commentId && r.UserId == userId);

            if (exists) return false;

            _context.CommentReactions.Add(new CommentReaction
            {
                CommentId = commentId,
                UserId = userId,
                IsThumbsUp = true
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
