using System.ComponentModel.DataAnnotations;

namespace CaixaDeBanco.Requests
{
    public class CreateAccountRequest
    {
        [Required(ErrorMessage = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Documento")]
        [MaxLength(14, ErrorMessage = "O documento deve ter 11 caracteres para CPF e 14 para CNPJ")]
        public string Document { get; set; } = string.Empty;
    }
}
