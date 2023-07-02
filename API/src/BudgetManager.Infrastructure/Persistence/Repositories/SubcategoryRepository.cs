using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        public void Add(Subcategory subcategory)
        {
            var category = InMemoryStorage.categories.FirstOrDefault(c => c.Id == subcategory.CategoryId);
            category.Subcategories.Add(subcategory);
        }

        public Subcategory? Get(Guid id)
        {
            return InMemoryStorage.categories.FirstOrDefault(c => c.Subcategories.Any(s => s.Id == id))?.Subcategories.First(sc => sc.Id == id);
        }

        public bool Delete(Guid id)
        {
            var category = InMemoryStorage.categories.FirstOrDefault(c => c.Subcategories.Any(sb => sb.Id == id));
            var subcategory = category.Subcategories.FirstOrDefault(s => s.Id == id);
            category.Subcategories.Remove(subcategory);

            return true;
        }

        public bool Update(Subcategory subcategory)
        {
            //W pamięci, więc brak implementacji
            return true;
        }
    }
}
