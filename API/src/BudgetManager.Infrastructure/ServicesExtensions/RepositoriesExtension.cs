using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Infrastructure.ServicesExtensions;
internal static class RepositoriesExtension
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>()
            .AddScoped<IExpenseSubcategoryRepository, ExpenseSubcategoryRepository>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IIncomeRepository, IncomeRepository>()
            .AddScoped<IIncomeCategoryRepository, IncomeCategoryRepository>()
            .AddScoped<IIncomeTableRepository, IncomeTableRepository>()
            .AddScoped<IExpenseTableRepository, ExpenseTableRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    } 
}
