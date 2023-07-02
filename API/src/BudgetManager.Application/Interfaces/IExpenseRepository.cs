using BudgetManager.Domain.Expenses;

namespace BudgetManager.Application.Interfaces
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);
        Expense? Get(Guid id);
        bool Edit(Expense expense);
        bool Delete(Guid id);
    }
}
