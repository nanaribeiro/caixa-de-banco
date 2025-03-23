
using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CaixaDeBanco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBankAccountController : ControllerBase
{
    private readonly IBankAccountHandler _bankAccountHandler;

    public CreateBankAccountController(IBankAccountHandler bankAccountHandler)
    {
        _bankAccountHandler = bankAccountHandler;
    }

    [HttpPost()]
    [EndpointSummary("Cria uma nova conta bancaria")]
    [EndpointName("Bank Account: Create")]
    [EndpointDescription("Cria uma nova conta bancaria")]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        return Ok(await _bankAccountHandler.CreateAccountAsync(request));
    }
}
