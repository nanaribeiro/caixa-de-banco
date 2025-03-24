using CaixaDeBanco.Database.Data;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Handlers;
using CaixaDeBanco.Models;
using CaixaDeBanco.Requests;
using CaixaDeBanco.Responses;
using Microsoft.EntityFrameworkCore;

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

        public Task<PagedResponse<List<GetAccountResponse>>> GetAccount(GetAccountRequest request)
        {
            try
            {
                var query = context
                .Account
                .AsNoTracking()
                .Where(x => (!string.IsNullOrEmpty(request.Document) && x.Document == request.Document)
                            || (!string.IsNullOrEmpty(request.Name) && x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase))
                            || (string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Document)))
                .Select(x => new GetAccountResponse()
                {
                    Balance = x.Balance,
                    OpenedAt = x.OpenedAt,
                    Name = x.Name,
                    Document = x.Document,
                    Status = x.Status
                });

                var accounts =  query
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();

                var count = query.Count();

                return Task.FromResult(new PagedResponse<List<GetAccountResponse>>(
                    accounts,
                    count,
                    request.PageNumber,
                    request.PageSize));
            }
            catch 
            {
                return Task.FromResult(new PagedResponse<List<GetAccountResponse>>
                    (null, 500, "Não foi possível consultar as contas"));
            }
        }
        public async Task<Response<string>> DeactivateAccountAsync(DeactivateAccountRequest request)
        {
            try
            {
                var account = context.Account.Where(context => context.Document == request.Document).Single();
                if (account.Status)
                {                   
                    account.Status = false;
                    context.Account.Update(account);
                    await context.SaveChangesAsync();

                    var history = new AccountHistory()
                    {
                        Account = account,
                        Document = account.Document,
                        User = Environment.UserName,
                        Action = Enumerator.EAccountAction.Inactivation,
                    };
                    await context.AccountHistory.AddAsync(history);
                    await context.SaveChangesAsync();

                    return new Response<string>(null, 200, "Conta inativada com sucesso!");
                }
                else
                {
                    return new Response<string>(null, 400, "Conta já está desativada!");
                }
            }
            catch
            {
                return new Response<string>(null, 500, "Não foi possível desativar a conta");                
            }
        }
    }
}

