using System.ComponentModel.DataAnnotations.Schema;
using CaixaDeBanco.Enumerator;
using CaixaDeBanco.Models;

namespace CaixaDeBanco.Database.Models
{
    public class TransactionHistory
    {
        public Guid Id { get; set; }
        public required BankingAccount AccountId { get; set; }
        public required ETransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? TransactionValue { get; set; }
    }
}
