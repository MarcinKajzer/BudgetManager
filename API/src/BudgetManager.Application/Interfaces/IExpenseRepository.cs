using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task CreateAsync(Expense expense, CancellationToken cancellationToken);
        Task<Expense?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Expense expense, CancellationToken cancellationToken);
        Task DeleteAsync(Expense expense, CancellationToken cancellationToken);
    }
}
