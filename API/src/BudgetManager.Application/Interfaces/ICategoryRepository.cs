using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        Category? Get(Guid id);
        IEnumerable<Category> GetAll();
        bool Update(Guid id, string name);
        bool Delete(Guid id);
    }
}
