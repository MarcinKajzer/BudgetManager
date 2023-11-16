using BudgetManager.Application.Interfaces;
using BudgetManager.Application.Security;
using BudgetManager.Infrastructure.Persistence;
using BudgetManager.Infrastructure.Persistence.Interceptors;
using BudgetManager.Infrastructure.Security;
using BudgetManager.Infrastructure.Services;
using BudgetManager.Infrastructure.ServicesExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAllOptions(configuration);
            services.AddAuth(configuration);

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).LogTo(Console.WriteLine, LogLevel.Information));

            services.AddRepositories();

            services.AddSingleton<IDateTime, DateTimeService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IPasswordManager, PasswordManager>();

            services.AddScoped<ITokenStorage, TokenStorage>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
