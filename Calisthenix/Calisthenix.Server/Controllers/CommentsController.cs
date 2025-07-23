using Calisthenix.Server.Data;
using Calisthenix.Server.Models.DTOs;
using Calisthenix.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calisthenix.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CalisthenixDbContext _context;

        public CommentsController(CalisthenixDbContext context)
        {
            _context = context;
        }

        [HttpGet("{exerciseId}")]
        public IActionResult GetComments(int exerciseId)
        {
            var comments = _context.Comments
                .Where(c => c.ExerciseId == exerciseId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    Username = c.User.Username,
                    CreatedAt = c.CreatedAt
                })
                .ToList();

            return Ok(comments);
        }

        [Authorize]
        [HttpPost("{exerciseId}")]
        public async Task<IActionResult> PostComment(int exerciseId, [FromBody] string content)
        {
            var username = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return Unauthorized();

            var comment = new Comment
            {
                Content = content,
                ExerciseId = exerciseId,
                UserId = user.Id
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
