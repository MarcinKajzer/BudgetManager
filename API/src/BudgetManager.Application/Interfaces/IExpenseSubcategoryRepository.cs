using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseSubcategoryRepository
    {
        Task CreateAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken);
        ExpenseSubcategory? Get(Guid id);
        Task UpdateAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken);
        Task DeleteAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken);
    }
}
