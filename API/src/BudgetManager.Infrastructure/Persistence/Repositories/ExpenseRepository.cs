using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        public void Add(Expense expense)
        {
            var subcategory = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Subcategories.Any(s => s.Id == expense.Subcategory.Id))?.Subcategories.First(sc => sc.Id == expense.Subcategory.Id);
            subcategory.Expenses.Add(expense);
        }
        public Expense? Get(Guid id)
        {
            var subcategory = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Subcategories.Any(s => s.Expenses.Any(e => e.Id == id)))?.Subcategories.First(sc => sc.Expenses.Any(e => e.Id == id));
            return subcategory.Expenses.FirstOrDefault(e => e.Id == id);
        }

        public bool Delete(Guid id)
        {
            var subcategory = InMemoryStorage.expenseCategories.FirstOrDefault(c => c.Subcategories.Any(s => s.Expenses.Any(e => e.Id == id)))?.Subcategories.First(sc => sc.Expenses.Any(e => e.Id == id));
            var expense = subcategory.Expenses.FirstOrDefault(e => e.Id == id);
            subcategory.Expenses.Remove(expense);

            return true;
        }

        public bool Edit(Expense expense)
        {
            return true;
        }
    }
}
