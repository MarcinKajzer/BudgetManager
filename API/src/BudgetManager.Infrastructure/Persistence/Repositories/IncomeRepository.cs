using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    internal class IncomeRepository : IIncomeRepository
    {
        public void Add(Income income)
        {
            var category = InMemoryStorage.incomeCategories.FirstOrDefault(c => c.Id == income.CategoryId);
            category.Incomes.Add(income);
        }
        public Income? Get(Guid id)
        {
            return InMemoryStorage.incomeCategories.FirstOrDefault(c => c.Incomes.Any(s => s.Id == id))?.Incomes.First(i => i.Id == id);
        }

        public bool Delete(Guid id)
        {
            var category = InMemoryStorage.incomeCategories.FirstOrDefault(c => c.Incomes.Any(s => s.Id == id));
            var income = category.Incomes.First(i => i.Id == id);
            category.Incomes.Remove(income);

            return true;
        }

        public bool Edit(Income expense)
        {
            return true;
        }
    }
}
