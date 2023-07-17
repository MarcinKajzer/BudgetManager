using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        void Add(ExpenseCategory category);
        ExpenseCategory? Get(Guid id);
        IEnumerable<ExpenseCategory> GetAll();
        bool Update(ExpenseCategory category);
        bool Delete(Guid id);
    }
}
