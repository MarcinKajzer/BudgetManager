using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category)
        {
            InMemoryStorage.categories.Add(category);
        }

        public Category? Get(Guid id)
        {
            return InMemoryStorage.categories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Category> GetAll() => InMemoryStorage.categories;

        public bool Update(Category category)
        {
            //Polityka 
            //Bez implementacji, bo na razie update w pamięci
            return true;
        }

        public bool Delete(Guid id)
        {
            var cat = InMemoryStorage.categories.FirstOrDefault(c => c.Id == id);

            if (cat is null)
            {
                return false;
            }

            InMemoryStorage.categories.Remove(cat);
            return true;
        }
    }
}
