using BudgetManager.Domain.Expenses;
using BudgetManager.Domain.Incomes;
using BudgetManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BudgetManager.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    //public DbSet<Income> Incomes { get; set; }
    //public DbSet<IncomeCategory> IncomeCategories { get; set; }
    //public DbSet<Expense> Expenses { get; set; }
    //public DbSet<ExpenseSubcategory> ExpenseSubcategories { get; set; }
    //public DbSet<ExpenseCategory> ExpenseCategories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntitySaveChangesInterceptor());
        base.OnConfiguring(builder);
    }
}
