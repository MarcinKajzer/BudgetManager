using BudgetManager.Domain.Expenses;
using BudgetManager.Domain.Incomes;
using BudgetManager.Domain.Users;
using BudgetManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BudgetManager.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _saveChangesInterceptor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
    }

    public DbSet<Income> Incomes { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseSubcategory> ExpenseSubcategory { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategory { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(_saveChangesInterceptor);
        builder.UseLazyLoadingProxies();
        base.OnConfiguring(builder);
    }
}
