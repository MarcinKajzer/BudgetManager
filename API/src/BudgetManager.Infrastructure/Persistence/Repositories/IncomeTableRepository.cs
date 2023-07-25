using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class IncomeTableRepository : IIncomeTableRepository
    {
        public IEnumerable<IncomeCategory> Get() => InMemoryStorage.incomeCategories;
    }
}
