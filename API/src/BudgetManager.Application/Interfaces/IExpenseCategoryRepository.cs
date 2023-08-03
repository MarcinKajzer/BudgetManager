using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        Task CreateAsync(ExpenseCategory category, CancellationToken cancellationToken);
        ExpenseCategory? Get(Guid id);
        IEnumerable<ExpenseCategory> GetAll();
        Task UpdateAsync(ExpenseCategory category, CancellationToken cancellationToken);
        Task DeleteAsync(ExpenseCategory category, CancellationToken cancellationToken);
    }
}
