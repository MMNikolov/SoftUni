namespace Calisthenix.Server.Services
{
    using System.Security.Cryptography;
    using System.Text;
    using Calisthenix.Server.Data;
    using Calisthenix.Server.Models;
    using Calisthenix.Server.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Calisthenix.Server.Models.DTOs;
    using Calisthenix.Server.Enums;
    using BCrypt.Net;

    public class AuthService : IAuthService
    {
        private readonly CalisthenixDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(CalisthenixDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> RegisterAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                throw new Exception("User already exists");

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthResult> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return AuthResult.NotFound;

            if (!BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                return AuthResult.InvalidPassword;

            user.PasswordHash = BCrypt.HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();

            return AuthResult.Success;
        }
    }
}
