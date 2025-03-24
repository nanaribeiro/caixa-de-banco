using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CaixaDeBanco.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAccountController(IBankAccountHandler bankAccountHandler) : ControllerBase
    {
        [HttpGet()]
        [EndpointSummary("Recupera todas as contas se não for informado nome ou documento. Caso seja informado o nome" +
                         " ou documento será filtrado pelo que for informado")]
        [EndpointName("Bank Account: Get")]
        [EndpointDescription("Recupera todas as contas se não for informado nome ou documento. Caso seja informado o nome" +
                              "ou documento será filtrado pelo que for informado")]
        public async Task<IActionResult> GetAccount([FromQuery] string? Name, string? Document)
        {
            return Ok(await bankAccountHandler.GetAccount(new GetAccountRequest()
            {
                Document = Document,
                Name = Name
            }));
        }
    }
}
