using KasirApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KasirApp.Data
{
    public class AppDbContext : DbContext   // ⚠️ BUKAN IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<MsUser> Users => Set<MsUser>();
        public DbSet<MsProduct> Products => Set<MsProduct>();
        public DbSet<LtPaymentMethod> PaymentMethods => Set<LtPaymentMethod>();
        public DbSet<TrTransaction> Transactions => Set<TrTransaction>();
        public DbSet<TrTransactionDetail> TransactionDetails => Set<TrTransactionDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique index username (sama seperti di SQL)
            modelBuilder.Entity<MsUser>()
                .HasIndex(u => u.Username).IsUnique();

            // Unique invoice
            modelBuilder.Entity<TrTransaction>()
                .HasIndex(t => t.InvoiceNo).IsUnique();

            // Index pencarian product name
            modelBuilder.Entity<MsProduct>()
                .HasIndex(p => p.Name);

            // Index history filter
            modelBuilder.Entity<TrTransaction>()
                .HasIndex(t => t.TransactionDate);
            modelBuilder.Entity<TrTransaction>()
                .HasIndex(t => t.IdUser);

            // Cascade detail saat transaction dihapus
            modelBuilder.Entity<TrTransactionDetail>()
                .HasOne(d => d.Transaction)
                .WithMany(t => t.Details)
                .HasForeignKey(d => d.IdTransaction)
                .OnDelete(DeleteBehavior.Cascade);

            // NO ACTION untuk user & product & payment (jaga history)
            modelBuilder.Entity<TrTransaction>()
                .HasOne(t => t.User).WithMany(u => u.Transactions)
                .HasForeignKey(t => t.IdUser).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrTransaction>()
                .HasOne(t => t.PaymentMethod)
                .WithMany()
                .HasForeignKey(t => t.IdPaymentMethod).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TrTransactionDetail>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.IdProduct).OnDelete(DeleteBehavior.NoAction);
        }

        // 🎯 Audit Trail otomatis
        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            ApplyAudit();
            return base.SaveChangesAsync(ct);
        }

        public override int SaveChanges()
        {
            ApplyAudit();
            return base.SaveChanges();
        }

        private void ApplyAudit()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User
                ?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? "SYSTEM";

            var now = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Stsrc = true;
                    entry.Entity.Datein = now;
                    entry.Entity.Userin = currentUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Dateup = now;
                    entry.Entity.Userup = currentUser;
                    entry.Property(e => e.Datein).IsModified = false;
                    entry.Property(e => e.Userin).IsModified = false;
                }
            }
        }
    }
}