using System.Reflection.Metadata;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Models;
using Microsoft.EntityFrameworkCore;
namespace CaixaDeBanco.Database.Data
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        public DbSet<BankingAccount> Account { get; set; } = default!;
        public DbSet<AccountHistory> AccountHistory { get; set; } = default!;
        public DbSet<TransactionHistory> TransactionHistory { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankingAccount>()
                .Property(b => b.OpenedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AccountHistory>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<TransactionHistory>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}