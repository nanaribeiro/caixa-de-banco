using CaixaDeBanco.Database.Data;
using CaixaDeBanco.Handlers;
using CaixaDeBanco.Models;
using CaixaDeBanco.Requests;
using CaixaDeBanco.Responses;

namespace CaixaDeBanco.Api.Handlers
{
    public class BankAccountHandler(BancoDbContext context) : IBankAccountHandler
    {
        public async Task<Response<BankingAccount?>> CreateAccountAsync(CreateAccountRequest request)
        {
            try
            {
                if(!context.Account.Any(account => account.Document == request.Document))
                {
                    var account = new BankingAccount()
                    {
                        Document = request.Document,
                        Name = request.Name,
                        Balance = 1000M,
                        Status = true
                    };
                    await context.Account.AddAsync(account);
                    await context.SaveChangesAsync();

                    return new Response<BankingAccount?>(account, 201, "Conta criada com sucesso!");
                }
                else
                {
                    return new Response<BankingAccount?>(null, 400, "Já existe uma conta para o documento informado!");
                }
            }
            catch
            {
                return new Response<BankingAccount?>(null, 500, "Não foi possível criar a conta");
            }
        }
    }
}
