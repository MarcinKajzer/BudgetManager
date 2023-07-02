using BudgetManager.Domain.Categories;

namespace BudgetManager.Application.Interfaces
{
    public interface ISubcategoryRepository
    {
        void Add(Subcategory subcategory);
        Subcategory? Get(Guid id);
        bool Update(Subcategory subcategory);
        bool Delete(Guid id);
    }
}
