using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseTableRepository : IExpenseTableRepository
    {
        public IEnumerable<ExpenseCategory> Get() => InMemoryStorage.expenseCategories;
    }
}
