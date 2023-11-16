using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

            return services;
        }
    }
}
