using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calisthenix.Server.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminExerciseController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Admin area is working");
        }

        [HttpDelete("{id}")]
        public IActionResult ForceDelete(int id)
        {
            // Example only — not connected yet
            return Ok($"Exercise with ID {id} would be deleted by Admin.");
        }
    }
}
