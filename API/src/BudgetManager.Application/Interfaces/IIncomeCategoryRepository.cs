using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeCategoryRepository
    {
        void Add(IncomeCategory category);
        IncomeCategory? Get(Guid id);
        IEnumerable<IncomeCategory> GetAll();
        bool Update(IncomeCategory category);
        bool Delete(Guid id);
    }
}
