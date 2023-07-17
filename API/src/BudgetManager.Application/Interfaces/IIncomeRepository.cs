using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeRepository
    {
        void Add(Income expense);
        Income? Get(Guid id);
        bool Edit(Income expense);
        bool Delete(Guid id);
    }
}
