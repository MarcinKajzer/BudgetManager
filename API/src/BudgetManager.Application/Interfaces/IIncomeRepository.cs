using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeRepository
    {
        Task CreateAsync(Income income, CancellationToken cancellationToken);
        Income? Get(Guid id);
        Task UpdateAsync(Income expense, CancellationToken cancellationToken);
        Task DeleteAsync(Income income, CancellationToken cancellationToken);
    }
}
