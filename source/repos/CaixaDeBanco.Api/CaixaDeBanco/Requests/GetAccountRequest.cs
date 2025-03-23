namespace CaixaDeBanco.Requests
{
    public class GetAccountRequest: PagedRequest
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
    }
}
