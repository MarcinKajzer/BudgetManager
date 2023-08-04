using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseTableRepository
    {
        IEnumerable<ExpenseCategory> Get(int year, int month);
    }
}
