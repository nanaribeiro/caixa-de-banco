using CaixaDeBanco.Requests;
using CaixaDeBanco.Responses;

namespace CaixaDeBanco.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<string>> TransferTransactionAsync(TransferTransactionRequest request);
    }
}
