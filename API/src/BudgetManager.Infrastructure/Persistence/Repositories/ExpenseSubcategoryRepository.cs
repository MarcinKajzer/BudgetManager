using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseSubcategoryRepository : IExpenseSubcategoryRepository
    {
        public void Add(ExpenseSubcategory subcategory)
        {
            var category = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Id == subcategory.CategoryId);
            category.Subcategories.Add(subcategory);
        }

        public ExpenseSubcategory? Get(Guid id)
        {
            return InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Subcategories.Any(s => s.Id == id))?.Subcategories.First(sc => sc.Id == id);
        }

        public bool Delete(Guid id)
        {
            var category = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Subcategories.Any(sb => sb.Id == id));
            var subcategory = category.Subcategories.FirstOrDefault(s => s.Id == id);
            category.Subcategories.Remove(subcategory);

            return true;
        }

        public bool Update(ExpenseSubcategory subcategory)
        {
            //W pamięci, więc brak implementacji
            return true;
        }
    }
}
