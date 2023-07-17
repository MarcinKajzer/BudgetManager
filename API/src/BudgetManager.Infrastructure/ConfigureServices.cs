using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddSingleton<IExpenseSubcategoryRepository, ExpenseSubcategoryRepository>();
            services.AddSingleton<IExpenseRepository, ExpenseRepository>();

            services.AddSingleton<IIncomeRepository, IncomeRepository>();
            services.AddSingleton<IIncomeCategoryRepository, IncomeCategoryRepository>();
           
            return services;
        }
    }
}
