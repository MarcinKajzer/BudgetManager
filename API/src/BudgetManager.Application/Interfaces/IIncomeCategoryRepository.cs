using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeCategoryRepository
    {
        Task CreateAsync(IncomeCategory category, CancellationToken cancellationToken);
        IncomeCategory? Get(Guid id);
        IEnumerable<IncomeCategory> GetAll();
        Task UpdateAsync(IncomeCategory category, CancellationToken cancellationToken);
        Task DeleteAsync(IncomeCategory category, CancellationToken cancellationToken);
    }
}
