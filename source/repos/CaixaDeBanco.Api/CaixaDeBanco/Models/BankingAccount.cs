using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaixaDeBanco.Database.Models;

namespace CaixaDeBanco.Models
{
    public class BankingAccount
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(14)]
        public required string Document { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNumber { get; set; }

        [Column(TypeName = "decimal(20, 2)")]
        public decimal Balance { get; set; }
        public DateTime OpenedAt { get; set; }
        public bool Status { get; set; }
        public List<AccountHistory> AccountHistories { get; set; } = [];
        public List<TransactionHistory> TransactionHistories { get; set; } = [];
        
    }
}
