using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseSubcategoryRepository
    {
        void Add(ExpenseSubcategory subcategory);
        ExpenseSubcategory? Get(Guid id);
        bool Update(ExpenseSubcategory subcategory);
        bool Delete(Guid id);
    }
}
