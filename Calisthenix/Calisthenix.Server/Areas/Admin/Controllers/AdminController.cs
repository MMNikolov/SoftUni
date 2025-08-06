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
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
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
