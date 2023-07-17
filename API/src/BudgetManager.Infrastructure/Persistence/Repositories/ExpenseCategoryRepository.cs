using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        public void Add(ExpenseCategory category)
        {
            InMemoryStorage.expenseCategories.Add(category);
        }

        public ExpenseCategory? Get(Guid id)
        {
            return InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ExpenseCategory> GetAll() => InMemoryStorage.expenseCategories;

        public bool Update(ExpenseCategory category)
        {
            //Polityka 
            //Bez implementacji, bo na razie update w pamięci
            return true;
        }

        public bool Delete(Guid id)
        {
            var cat = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Id == id);

            if (cat is null)
            {
                return false;
            }

            InMemoryStorage.expenseCategories.Remove(cat);
            return true;
        }
    }
}
