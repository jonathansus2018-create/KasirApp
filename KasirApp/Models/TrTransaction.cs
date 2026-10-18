using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KasirApp.Models
{
    [Table("TrTransaction")]
    public class TrTransaction : IAuditable
    {
        [Key]
        [Column("IdTransaction")]
        [StringLength(36)]
        public string IdTransaction { get; set; } = Guid.NewGuid().ToString();

        [Column("InvoiceNo")]
        [Required, StringLength(20)]
        public string InvoiceNo { get; set; } = string.Empty;

        [Column("TransactionDate")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Column("IdUser")]
        [Required, StringLength(5)]
        public string IdUser { get; set; } = string.Empty;

        [ForeignKey(nameof(IdUser))]
        public MsUser? User { get; set; }

        [Column("IdPaymentMethod")]
        public int IdPaymentMethod { get; set; }

        [ForeignKey(nameof(IdPaymentMethod))]
        public LtPaymentMethod? PaymentMethod { get; set; }

        [Column("TotalPrice", TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Column("Stsrc")] public bool Stsrc { get; set; } = true;
        [Column("Datein")] public DateTime Datein { get; set; } = DateTime.Now;
        [Column("Userin")] public string Userin { get; set; } = "SYSTEM";
        [Column("Dateup")] public DateTime? Dateup { get; set; }
        [Column("Userup")] public string? Userup { get; set; }

        public ICollection<TrTransactionDetail> Details { get; set; } = new List<TrTransactionDetail>();
    }
}