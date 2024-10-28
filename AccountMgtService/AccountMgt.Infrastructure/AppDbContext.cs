using AccountMgt.Models.Models;
using AccountMgt.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountMgt.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DetailedTransactionReport> DetailedTransactionReports { get; set; }


    }
}
