namespace Calisthenix.Server.Services.Interfaces
{
    using Calisthenix.Server.Enums;
    using Calisthenix.Server.Models.DTOs;

    public interface IAuthService
    {
        Task<string> RegisterAsync(string username, string password);
        Task<string> LoginAsync(string username, string password);
        Task<AuthResult> ChangePasswordAsync(int userId, ChangePasswordRequest request);

    }
}
