namespace Calisthenix.Server.Controllers
{
    using System.Security.Claims;
    using Calisthenix.Server.Models.DTOs;
    using Calisthenix.Server.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Calisthenix.Server.Enums;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDTO dto)
        {
            var token = await _authService.RegisterAsync(dto.Username, dto.Password);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto.Username, dto.Password);
            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user ID");

            var result = await _authService.ChangePasswordAsync(userId, request);

            return result switch
            {
                AuthResult.Success => Ok("Password changed successfully."),
                AuthResult.NotFound => NotFound("User not found."),
                AuthResult.InvalidPassword => BadRequest("Current password is incorrect."),
                _ => StatusCode(500, "An unexpected error occurred.")
            };
        }
    }
}