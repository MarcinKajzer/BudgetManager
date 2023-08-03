using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task CreateAsync(Expense expense, CancellationToken cancellationToken);
        Expense? Get(Guid id);
        Task UpdateAsync(Expense expense, CancellationToken cancellationToken);
        Task DeleteAsync(Expense expense, CancellationToken cancellationToken);
    }
}
