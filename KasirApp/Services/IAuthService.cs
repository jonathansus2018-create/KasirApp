using KasirApp.Models;

namespace KasirApp.Services
{
    public interface IAuthService
    {
        Task<MsUser?> ValidateAsync(string username, string password);
        Task<(bool Success, string? Error, MsUser? User)> RegisterAsync(
            string username, string fullName, string password, string role);
        Task<MsUser?> GetByIdAsync(string idUser);
    }
}