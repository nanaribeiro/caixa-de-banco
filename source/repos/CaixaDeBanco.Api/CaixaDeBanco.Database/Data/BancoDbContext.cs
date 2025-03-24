using System.Reflection.Metadata;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace CaixaDeBanco.Database.Data
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }
        public BancoDbContext() { }

        public virtual DbSet<BankingAccount> Account { get; set; } = default!;
        public virtual DbSet<AccountHistory> AccountHistory { get; set; } = default!;
        public virtual DbSet<TransactionHistory> TransactionHistory { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankingAccount>()
                .Property(b => b.OpenedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AccountHistory>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<BankingAccount>()
                .Property(b => b.AccountNumber)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<TransactionHistory>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}