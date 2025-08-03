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
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetComments(int exerciseId)
        {
            int? userId = User.Identity?.IsAuthenticated == true
            ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            : null;

            var comments = await _commentService.GetCommentsForExerciseAsync(exerciseId, userId);
            return Ok(comments);
        }

        [Authorize]
        [HttpPost("{exerciseId}")]
        public async Task<IActionResult> PostComment(int exerciseId, [FromBody] CreateCommentDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _commentService.AddCommentAsync(exerciseId, userId, dto.Content);
            return Ok(result);
        }

        [HttpPost("react")]
        [Authorize]
        public async Task<IActionResult> ReactToComment([FromBody] CommentReactionDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var success = await _commentService.ToggleReactionAsync(dto.CommentId, userId);
            if (!success)
                return BadRequest("Already reacted to this comment.");

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
