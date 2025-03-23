using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaixaDeBanco.Enumerator;
using CaixaDeBanco.Models;

namespace CaixaDeBanco.Database.Models
{
    public class AccountHistory
    {
        public Guid Id { get; set; }
        public EAccountAction Action { get; set; }
        public required BankingAccount AccountId { get; set; }

        [MaxLength(14)]
        public required string Document { get; set; }
        public DateTime CreatedAt { get; set; }

        [MaxLength(100)]
        public string? User { get; set; }
    }
}
