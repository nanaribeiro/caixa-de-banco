using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CaixaDeBanco.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferTransactionController(ITransactionHandler transactionHandler) : ControllerBase
    {
        [HttpPost()]
        [EndpointSummary("Faz transferência entre contas bancárias")]
        [EndpointName("Transaction: Transfer")]
        [EndpointDescription("Faz transferência entre contas bancárias")]
        public async Task<IActionResult> TransferAccountTransaction(TransferTransactionRequest request)
        {
            var result = await transactionHandler.TransferTransactionAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
