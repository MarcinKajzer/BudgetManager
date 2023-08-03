using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure.Persistence;
using BudgetManager.Infrastructure.Persistence.Interceptors;
using BudgetManager.Infrastructure.Persistence.Repositories;
using BudgetManager.Infrastructure.Services;
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
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).LogTo(Console.WriteLine, LogLevel.Information));

            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<IExpenseSubcategoryRepository, ExpenseSubcategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IIncomeCategoryRepository, IncomeCategoryRepository>();

            services.AddScoped<IIncomeTableRepository, IncomeTableRepository>();
            services.AddScoped<IExpenseTableRepository, ExpenseTableRepository>();

            services.AddSingleton<IDateTime, DateTimeService>();
            return services;
        }
    }
}
