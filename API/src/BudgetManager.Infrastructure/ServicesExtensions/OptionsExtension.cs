using BudgetManager.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure.ServicesExtensions;
internal static class OptionsExtension
{
    internal static IServiceCollection AddAllOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(AuthOptions.Section));

        return services;
    }
}
