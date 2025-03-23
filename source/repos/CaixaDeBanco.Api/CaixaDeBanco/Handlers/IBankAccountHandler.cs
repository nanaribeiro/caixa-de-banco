using CaixaDeBanco.Models;
using CaixaDeBanco.Requests;
using CaixaDeBanco.Responses;

namespace CaixaDeBanco.Handlers
{
    public interface IBankAccountHandler
    {
        Task<Response<BankingAccount?>> CreateAccountAsync(CreateAccountRequest request);
        Task<PagedResponse<List<GetAccountResponse>>> GetAccountAsync(GetAccountRequest request);
    }
}
