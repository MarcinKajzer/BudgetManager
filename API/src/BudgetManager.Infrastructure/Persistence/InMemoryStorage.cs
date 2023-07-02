using BudgetManager.Domain.Categories;

namespace BudgetManager.Infrastructure.Persistence
{
    internal class InMemoryStorage
    {
        public static List<Category> categories = new List<Category>
        {
            new Category { Name = "Dom", Id = Guid.NewGuid() },
            new Category { Name = "Zdrowie", Id = Guid.NewGuid() },
            new Category { Name = "Transport", Id = Guid.NewGuid() },
            new Category { Name = "Rozwój osobisty", Id = Guid.NewGuid() }
        };
    }
}
