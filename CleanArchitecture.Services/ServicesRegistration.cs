using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace CleanArchitecture.Services
{
    public static class ServicesRegistration
    {
        public static void AddServiceRegistrations(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICarServices, CarServices>();
            builder.Services.AddTransient<IAccountsService, AccountsService>();
        }
    }
}
