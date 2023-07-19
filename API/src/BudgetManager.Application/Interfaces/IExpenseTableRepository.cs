using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseTableRepository
    {
        IEnumerable<ExpenseCategory> Get();
    }
}
