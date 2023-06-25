using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category { Name = "Dom", Id = Guid.NewGuid() },
            new Category { Name = "Zdrowie", Id = Guid.NewGuid() },
            new Category { Name = "Transport", Id = Guid.NewGuid() },
            new Category { Name = "Rozwój osobisty", Id = Guid.NewGuid() }
        };

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public Category? Get(Guid id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Category> GetAll() => _categories;

        public bool Update(Guid id, string name)
        {
            var cat = _categories.FirstOrDefault(c => c.Id == id);

            if (cat is null)
            {
                return false;
            }

            cat.Name = name;
            return true;
        }

        public bool Delete(Guid id)
        {
            var cat = _categories.FirstOrDefault(c => c.Id == id);

            if (cat is null)
            {
                return false;
            }

            _categories.Remove(cat);
            return true;
        }
    }
}
