using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaixaDeBanco.Database.Models
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

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Balance { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime OpenedAt { get; set; }
        public bool Status { get; set; }
        public List<AccountHistory> AccountHistories { get; set; } = [];
        public List<TransactionHistory> TransactionHistories { get; set; } = [];
        
    }
}
