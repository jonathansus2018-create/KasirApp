using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KasirApp.Models
{
    [Table("TrTransactionDetail")]
    public class TrTransactionDetail : IAuditable
    {
        [Key]
        [Column("IdTransactionDetail")]
        [StringLength(36)]
        public string IdTransactionDetail { get; set; } = Guid.NewGuid().ToString();

        [Column("IdTransaction")]
        [StringLength(36)]
        public string IdTransaction { get; set; } = string.Empty;

        [ForeignKey(nameof(IdTransaction))]
        public TrTransaction? Transaction { get; set; }

        [Column("IdProduct")]
        [StringLength(36)]
        public string IdProduct { get; set; } = string.Empty;

        [ForeignKey(nameof(IdProduct))]
        public MsProduct? Product { get; set; }

        [Column("Qty")]
        public int Qty { get; set; }

        [Column("Price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("Discount", TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }

        [Column("SubTotal", TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column("Stsrc")] public bool Stsrc { get; set; } = true;
        [Column("Datein")] public DateTime Datein { get; set; } = DateTime.Now;
        [Column("Userin")] public string Userin { get; set; } = "SYSTEM";
        [Column("Dateup")] public DateTime? Dateup { get; set; }
        [Column("Userup")] public string? Userup { get; set; }
    }
}