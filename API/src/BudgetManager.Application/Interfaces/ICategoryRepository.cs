using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        Category? Get(Guid id);
        IEnumerable<Category> GetAll();
        bool Update(Category category);
        bool Delete(Guid id);
    }
}
