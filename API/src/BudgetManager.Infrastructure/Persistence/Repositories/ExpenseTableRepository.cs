using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseTableRepository : IExpenseTableRepository
    {
        public IEnumerable<ExpenseCategory> Get() => InMemoryStorage.expenseCategories;
    }
}
