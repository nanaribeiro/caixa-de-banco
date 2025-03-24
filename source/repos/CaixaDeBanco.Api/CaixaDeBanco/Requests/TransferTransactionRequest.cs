using System.ComponentModel.DataAnnotations;

namespace CaixaDeBanco.Requests
{
    public class TransferTransactionRequest
    {
        [Required(ErrorMessage = "Conta de origem")]
        public string OriginAccountNumber { get; set; } = string.Empty!;
        [Required(ErrorMessage = "Conta de destino")]
        public string DestinationAccountNumber { get; set; } = string.Empty!;
        [Required(ErrorMessage = "Valor da transferência")]
        public decimal Value { get; set; }
    }
}
