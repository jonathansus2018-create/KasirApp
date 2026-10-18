using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KasirApp.Models
{
    [Table("MsProduct")]
    public class MsProduct : IAuditable
    {
        [Key]
        [Column("IdProduct")]
        [StringLength(36)]
        public string IdProduct { get; set; } = Guid.NewGuid().ToString();

        [Column("Name")]
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("Weight", TypeName = "decimal(10,2)")]
        public decimal Weight { get; set; }

        [Column("Stock")]
        public int Stock { get; set; }

        [Column("Price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("Discount", TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }

        [Column("Stsrc")] public bool Stsrc { get; set; } = true;
        [Column("Datein")] public DateTime Datein { get; set; } = DateTime.Now;
        [Column("Userin")] public string Userin { get; set; } = "SYSTEM";
        [Column("Dateup")] public DateTime? Dateup { get; set; }
        [Column("Userup")] public string? Userup { get; set; }
    }
}