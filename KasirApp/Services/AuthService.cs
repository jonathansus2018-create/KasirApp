//using BCrypt.Net;
using KasirApp.Data;
using KasirApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KasirApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db) => _db = db;

        public async Task<MsUser?> ValidateAsync(string username, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Stsrc);
            if (user == null) return null;

            // BCrypt.Verify otomatis cocokkan hash & salt
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        public async Task<(bool Success, string? Error, MsUser? User)> RegisterAsync(
            string username, string fullName, string password, string role)
        {
            if (await _db.Users.AnyAsync(u => u.Username == username))
                return (false, "Username sudah dipakai.", null);

            if (role != "Admin" && role != "Cashier")
                return (false, "Role tidak valid.", null);

            var user = new MsUser
            {
                IdUser = await GenerateUserIdAsync(),
                Username = username,
                FullName = fullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role,
                Stsrc = true,
                Datein = DateTime.Now,
                Userin = "SYSTEM"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return (true, null, user);
        }

        public Task<MsUser?> GetByIdAsync(string idUser) =>
            _db.Users.FirstOrDefaultAsync(u => u.IdUser == idUser && u.Stsrc);

        // Generate 5 digit: 00001, 00002, ...
        private async Task<string> GenerateUserIdAsync()
        {
            var lastId = await _db.Users
                .OrderByDescending(u => u.IdUser)
                .Select(u => u.IdUser)
                .FirstOrDefaultAsync();

            int next = lastId == null ? 1 : int.Parse(lastId) + 1;
            return next.ToString("D5");
        }
    }
}