using Calisthenix.Server.Data;
using Calisthenix.Server.Models;
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

        public async Task<Comment> AddCommentAsync(int exerciseId, int userId, string content)
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
            return comment;
        }

        public async Task<List<Comment>> GetCommentsForExerciseAsync(int exerciseId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Reactions)
                .Where(c => c.ExerciseId == exerciseId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> ToggleReactionAsync(int commentId, int userId)
        {
            var existing = await _context.CommentReactions
                .FirstOrDefaultAsync(r => r.CommentId == commentId && r.UserId == userId);

            if (existing != null)
            {
                _context.CommentReactions.Remove(existing);
                await _context.SaveChangesAsync();
                return false;
            }

            _context.CommentReactions.Add(new CommentReaction
            {
                CommentId = commentId,
                UserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
