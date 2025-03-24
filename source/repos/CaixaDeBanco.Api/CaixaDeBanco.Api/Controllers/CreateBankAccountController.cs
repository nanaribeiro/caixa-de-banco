
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
        var result = await bankAccountHandler.CreateAccountAsync(request);
        return result.IsSuccess ? Created(string.Empty, result) : BadRequest(result);
    }
}
