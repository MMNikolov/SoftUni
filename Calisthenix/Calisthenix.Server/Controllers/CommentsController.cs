using Calisthenix.Server.Data;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Calisthenix.Server.Services.Interfaces;

namespace Calisthenix.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CalisthenixDbContext _context;
        private readonly ICommentService _commentService;

        public CommentsController(CalisthenixDbContext context, ICommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetComments(int exerciseId)
        {
            var userId = User.Identity?.IsAuthenticated == true
                ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                : (int?)null;

            var comments = await _commentService.GetCommentsForExerciseAsync(exerciseId);

            var dto = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Content = c.Content,
                Username = c.User?.Username ?? "Anonymous",
                CreatedAt = c.CreatedAt,
                ThumbsUpCount = c.Reactions?.Count(r => r.IsThumbsUp) ?? 0,
                LikedByCurrentUser = userId.HasValue && c.Reactions.Any(r => r.UserId == userId.Value && r.IsThumbsUp)
            }).ToList();

            return Ok(dto);
        }

        [Authorize]
        [HttpPost("{exerciseId}")]
        public async Task<IActionResult> PostComment(int exerciseId, [FromBody] string content)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var comment = await _commentService.AddCommentAsync(exerciseId, userId, content);
            return Ok(new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                Username = User.Identity?.Name ?? "You",
                CreatedAt = comment.CreatedAt,
                ThumbsUpCount = 0
            });
        }

        [HttpPost("react")]
        [Authorize]
        public async Task<IActionResult> ReactToComment([FromBody] CommentReactionDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
            var existing = await _context.CommentReactions
                .FirstOrDefaultAsync(r => r.CommentId == dto.CommentId && r.UserId == userId);
        
            if (existing != null)
            {
                return BadRequest("You already reacted to this comment.");
            }
        
            var reaction = new CommentReaction
            {
                CommentId = dto.CommentId,
                UserId = userId,
                IsThumbsUp = true
            };
        
            _context.CommentReactions.Add(reaction);
            await _context.SaveChangesAsync();
        
            return Ok(new { message = "Thumbs up added." });
        }

        [HttpPost("{commentId}/like")]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int commentId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var liked = await _commentService.ToggleReactionAsync(commentId, userId);

            return Ok(new { liked });
        }
    }
}
