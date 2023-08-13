using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeRepository
    {
        Task CreateAsync(Income income, CancellationToken cancellationToken);
        Task<Income?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Income expense, CancellationToken cancellationToken);
        Task DeleteAsync(Income income, CancellationToken cancellationToken);
    }
}
