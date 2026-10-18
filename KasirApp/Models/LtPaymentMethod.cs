using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KasirApp.Models
{
    [Table("LtPaymentMethod")]
    public class LtPaymentMethod : IAuditable
    {
        [Key]
        [Column("IdPaymentMethod")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPaymentMethod { get; set; }

        [Column("MethodName")]
        [Required, StringLength(50)]
        public string MethodName { get; set; } = string.Empty;

        [Column("Stsrc")] public bool Stsrc { get; set; } = true;
        [Column("Datein")] public DateTime Datein { get; set; } = DateTime.Now;
        [Column("Userin")] public string Userin { get; set; } = "SYSTEM";
        [Column("Dateup")] public DateTime? Dateup { get; set; }
        [Column("Userup")] public string? Userup { get; set; }
    }
}