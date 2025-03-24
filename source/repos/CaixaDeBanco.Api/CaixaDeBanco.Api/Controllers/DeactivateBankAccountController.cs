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
            var result = await bankAccountHandler.DeactivateAccountAsync(new DeactivateAccountRequest() { Document = document});
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
