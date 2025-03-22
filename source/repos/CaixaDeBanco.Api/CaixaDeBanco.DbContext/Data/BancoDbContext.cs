using CaixaDeBanco.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace CaixaDeBanco.Database.Data
{
    public class BancoDbContext : DbContext
    {
        public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

        public DbSet<BankingAccount> Account { get; set; } = default!;
        public DbSet<AccountHistory> AccountHistory { get; set; } = default!;
        public DbSet<TransactionHistory> TransactionHistory { get; set; } = default!;
    }
}