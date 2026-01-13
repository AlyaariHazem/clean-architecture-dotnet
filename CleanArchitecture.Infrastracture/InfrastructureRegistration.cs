using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastracture.Persistence;
using CleanArchitecture.Infrastracture.Persistence.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastracture
{
    /// <summary>
    /// Infrastructure layer service registration extension methods
    /// </summary>
    public static class InfrastructureRegistration
    {
        /// <summary>
        /// Registers infrastructure services including Unit of Work and repositories
        /// </summary>
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder, string connectionString)
        {
            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Unit of Work (scoped per HTTP request)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Note: Repositories are created by UnitOfWork, so we don't register them directly
            // This ensures all repositories in a request share the same DbContext instance
        }

        /// <summary>
        /// Alternative registration when DbContext is already registered elsewhere
        /// </summary>
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            // Register Unit of Work (scoped per HTTP request)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
