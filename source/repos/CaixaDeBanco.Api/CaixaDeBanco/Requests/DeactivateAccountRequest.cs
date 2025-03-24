using System.ComponentModel.DataAnnotations;

namespace CaixaDeBanco.Requests
{
    public class DeactivateAccountRequest
    {
        [Required(ErrorMessage = "Documento")]
        public string Document { get; set; } = string.Empty;
    }
}
