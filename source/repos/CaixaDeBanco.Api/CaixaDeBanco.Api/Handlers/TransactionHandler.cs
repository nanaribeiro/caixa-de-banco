using CaixaDeBanco.Database.Data;
using CaixaDeBanco.Database.Models;
using CaixaDeBanco.Handlers;
using CaixaDeBanco.Requests;
using CaixaDeBanco.Responses;

namespace CaixaDeBanco.Api.Handlers
{
    public class TransactionHandler(BancoDbContext context) : ITransactionHandler
    {
        public async Task<Response<string>> TransferTransactionAsync(TransferTransactionRequest request)
        {
			try
			{
                var originAccount = context.Account.Where(context => context.AccountNumber == int.Parse(request.OriginAccountNumber)).SingleOrDefault();
                var destinationAccount = context.Account.Where(context => context.AccountNumber == int.Parse(request.DestinationAccountNumber)).SingleOrDefault();
                if(originAccount == null)
                {
                    return new Response<string>(null, 404, "Conta de origem não encontrada");
                }
                if(destinationAccount == null)
                {
                    return new Response<string>(null, 404, "Conta de destino não encontrada");
                }
                if (!originAccount.Status)
                {
                    return new Response<string>(null, 400, "Conta de origem desativada");
                }
                if (!destinationAccount.Status)
                {
                    return new Response<string>(null, 400, "Conta de destino desativada");
                }
                if(originAccount.Balance < request.Value)
                {
                    return new Response<string>(null, 400, "Saldo insuficiente");
                }
                originAccount.Balance -= request.Value;
                destinationAccount.Balance += request.Value;
                context.Account.Update(originAccount);
                context.Account.Update(destinationAccount);
                await context.SaveChangesAsync();

                var transaction = new TransactionHistory()
                {
                    Account = originAccount,
                    TransactionType = Enumerator.ETransactionType.Transfer,
                    TransactionValue = request.Value,
                };
                context.TransactionHistory.Add(transaction);
                await context.SaveChangesAsync();
                return new Response<string>(null, 200, "Transferência feita com sucesso!");
            }
            catch
			{
                return new Response<string>(null, 500, "A transferência não pode ser feita");
            }
        }
    }
}

