using CleanArchitecture.Infrastracture.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastracture
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient(typeof(IRepository<>), typeof(IRepository<>));
        }
    }
}
