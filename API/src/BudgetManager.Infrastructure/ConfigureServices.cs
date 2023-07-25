using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure.Persistence;
using BudgetManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddSingleton<IExpenseSubcategoryRepository, ExpenseSubcategoryRepository>();
            services.AddSingleton<IExpenseRepository, ExpenseRepository>();

            services.AddSingleton<IIncomeRepository, IncomeRepository>();
            services.AddSingleton<IIncomeCategoryRepository, IncomeCategoryRepository>();

            services.AddSingleton<IIncomeTableRepository, IncomeTableRepository>();
            services.AddSingleton<IExpenseTableRepository, ExpenseTableRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
