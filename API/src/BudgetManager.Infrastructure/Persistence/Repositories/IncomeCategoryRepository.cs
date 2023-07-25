using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        public void Add(IncomeCategory category)
        {
            InMemoryStorage.incomeCategories.Add(category);
        }

        public IncomeCategory? Get(Guid id)
        {
            return InMemoryStorage.incomeCategories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<IncomeCategory> GetAll() => InMemoryStorage.incomeCategories;

        public bool Update(IncomeCategory category)
        {
            //Polityka 
            //Bez implementacji, bo na razie update w pamięci
            return true;
        }

        public bool Delete(Guid id)
        {
            var cat = InMemoryStorage.incomeCategories.FirstOrDefault(c => c.Id == id);

            if (cat is null)
            {
                return false;
            }

            InMemoryStorage.incomeCategories.Remove(cat);
            return true;
        }
    }
}
