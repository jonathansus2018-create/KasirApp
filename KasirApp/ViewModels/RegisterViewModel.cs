using System.ComponentModel.DataAnnotations;

namespace KasirApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(300)]
        public string FullName { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        // ⚠️ Di produksi, hanya Admin yang bisa pilih role saat register user baru.
        // Untuk demo/mahasiswa boleh; nanti aku tunjukkan cara batasi.
        [Required]
        public string Role { get; set; } = "Cashier"; // "Admin" | "Cashier"
    }
}