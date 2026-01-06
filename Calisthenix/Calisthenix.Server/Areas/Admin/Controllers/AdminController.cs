using Calisthenix.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly CalisthenixDbContext _context;

    public AdminController(CalisthenixDbContext context)
    {
        _context = context;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.Users
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Role
            }).ToListAsync();

        return Ok(users.ToList());
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        using var tx = await _context.Database.BeginTransactionAsync();

        try
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == id);
            if (!userExists) return NotFound();

            await _context.CommentReactions
                .Where(r => r.UserId == id)
                .ExecuteDeleteAsync();

            var exerciseIds = await _context.Exercises
                .Where(e => e.UserId == id)
                .Select(e => e.Id)
                .ToListAsync();

            if (exerciseIds.Count > 0)
            {
                var commentIds = await _context.Comments
                    .Where(c => exerciseIds.Contains(c.ExerciseId))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (commentIds.Count > 0)
                {
                    await _context.CommentReactions
                        .Where(r => commentIds.Contains(r.CommentId))
                        .ExecuteDeleteAsync();

                    await _context.Comments
                        .Where(c => commentIds.Contains(c.Id))
                        .ExecuteDeleteAsync();
                }

                await _context.WorkoutExercises
                    .Where(we => exerciseIds.Contains(we.ExerciseId))
                    .ExecuteDeleteAsync();

                await _context.Exercises
                    .Where(e => exerciseIds.Contains(e.Id))
                    .ExecuteDeleteAsync();

                await _context.Workouts
                    .Where(w => exerciseIds.Contains(w.Id))
                    .ExecuteDeleteAsync();
            }

            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            await tx.CommitAsync();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            await tx.RollbackAsync();
            return Conflict(new
            {
                message = "Cannot delete user due to related data. Clean up dependents first.",
                detail = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

    [HttpPut("users/{id}/promote")]
    public async Task<IActionResult> PromoteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Role = "Admin";
        await _context.SaveChangesAsync();

        return Ok(new { message = "User promoted to Admin." });
    }
}
