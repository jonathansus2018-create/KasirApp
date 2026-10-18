using KasirApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KasirApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<AppDbContext>();

            // 1. Seed Admin default (kalau belum ada)
            if (!await db.Users.AnyAsync(u => u.Role == "Admin"))
            {
                db.Users.Add(new MsUser
                {
                    IdUser = "00001",
                    Username = "admin",
                    FullName = "Super Admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    Stsrc = true,
                    Datein = DateTime.Now,
                    Userin = "SYSTEM"
                });
                await db.SaveChangesAsync();
            }

            // 2. Payment Method — SKIP kalau SQL sudah insert
            // (Cek dulu apakah SQL-mu sudah dijalankan)
            if (!await db.PaymentMethods.AnyAsync())
            {
                db.PaymentMethods.AddRange(
                    new LtPaymentMethod { MethodName = "Cash", Stsrc = true, Datein = DateTime.Now, Userin = "SYSTEM" },
                    new LtPaymentMethod { MethodName = "Debit", Stsrc = true, Datein = DateTime.Now, Userin = "SYSTEM" },
                    new LtPaymentMethod { MethodName = "Credit", Stsrc = true, Datein = DateTime.Now, Userin = "SYSTEM" }
                );
                await db.SaveChangesAsync();
            }
        }
    }
}