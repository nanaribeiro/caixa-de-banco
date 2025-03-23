
using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CaixaDeBanco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBankAccountController(IBankAccountHandler bankAccountHandler) : ControllerBase
{
    [HttpPost()]
    [EndpointSummary("Cria uma nova conta bancaria")]
    [EndpointName("Bank Account: Create")]
    [EndpointDescription("Cria uma nova conta bancaria")]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        return Ok(await bankAccountHandler.CreateAccountAsync(request));
    }
}
