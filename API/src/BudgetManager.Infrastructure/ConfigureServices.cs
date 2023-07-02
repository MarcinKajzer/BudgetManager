using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ISubcategoryRepository, SubcategoryRepository>();

            return services;
        }
    }
}
