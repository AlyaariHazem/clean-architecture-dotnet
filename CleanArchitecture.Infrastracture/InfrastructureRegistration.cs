using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastracture.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastracture
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
