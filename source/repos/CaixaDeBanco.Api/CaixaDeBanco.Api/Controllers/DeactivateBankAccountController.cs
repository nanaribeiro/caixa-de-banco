using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CaixaDeBanco.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeactivateBankAccountController(IBankAccountHandler bankAccountHandler) : ControllerBase
    {
        [HttpPatch()]
        [EndpointSummary("Inativa uma conta bancaria")]
        [EndpointName("Bank Account: Deactivate")]
        [EndpointDescription("Inativa uma conta bancaria")]
        public async Task<IActionResult> DeactivateAccount([FromQuery] string document)
        {
            return Ok(await bankAccountHandler.DeactivateAccountAsync(new DeactivateAccountRequest() { Document = document}));
        }
    }
}
