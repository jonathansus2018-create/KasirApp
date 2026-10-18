using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KasirApp.Models
{
    [Table("MsUser")]
    public class MsUser : IAuditable
    {
        [Key]
        [Column("IdUser")]
        [StringLength(5)]
        public string IdUser { get; set; } = string.Empty;

        [Column("Username")]
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Column("FullName")]
        [Required, StringLength(300)]
        public string FullName { get; set; } = string.Empty;

        [Column("PasswordHash")]
        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("Role")]
        [Required, StringLength(20)]
        public string Role { get; set; } = "Cashier"; // "Admin" | "Cashier"

        [Column("Stsrc")] public bool Stsrc { get; set; } = true;
        [Column("Datein")] public DateTime Datein { get; set; } = DateTime.Now;
        [Column("Userin")] public string Userin { get; set; } = "SYSTEM";
        [Column("Dateup")] public DateTime? Dateup { get; set; }
        [Column("Userup")] public string? Userup { get; set; }

        public ICollection<TrTransaction> Transactions { get; set; } = new List<TrTransaction>();
    }
}