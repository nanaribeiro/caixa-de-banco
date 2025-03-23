using CaixaDeBanco.Database.Data;
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

        public async Task<PagedResponse<List<GetAccountResponse>>> GetAccountAsync(GetAccountRequest request)
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

                var accounts = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<GetAccountResponse>>(
                    accounts,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<GetAccountResponse>>(null, 500, "Não foi possível consultar as contas");
            }
        }
    }
}
