using CaixaDeBanco.Api.Handlers;
using CaixaDeBanco.Handlers;

namespace CaixaDeBanco.Api
{
    public static class IHandlerInitializer
    {
        public static IServiceCollection AddHandlerDependencyGroup(
             this IServiceCollection services)
        {
            services.AddScoped<IBankAccountHandler, BankAccountHandler>();

            return services;
        }
    }
}
