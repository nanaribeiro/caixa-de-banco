using System.ComponentModel.DataAnnotations.Schema;
using CaixaDeBanco.Enumerator;

namespace CaixaDeBanco.Database.Models
{
    public class TransactionHistory
    {
        public Guid Id { get; set; }
        public required BankingAccount AccountId { get; set; }
        public required ETransactionType TransactionType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }


        [Column(TypeName = "decimal(5, 2)")]
        public decimal? TransactionValue { get; set; }
    }
}
