namespace CaixaDeBanco.Responses
{
    public class GetAccountResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime OpenedAt { get; set; }
        public bool Status { get; set; }
    }
}
