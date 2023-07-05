using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence
{
    internal class InMemoryStorage
    {
        public static List<Category> categories = new List<Category>
        {
            new Category { Name = "Dom", Id = Guid.NewGuid() },
            new Category { Name = "Zdrowie", Id = Guid.NewGuid() },
            new Category { Name = "Transport", Id = Guid.NewGuid() },
            new Category 
            { 
                Name = "Rozwój osobisty", 
                Id = Guid.NewGuid(), 
                Subcategories = new List<Subcategory>
                {
                    new Subcategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Książki",
                        Expenses = new List<Expense>
                        {
                            new Expense
                            {
                                Id = Guid.NewGuid(),
                                Amount = 12.14m,
                                Comment = "No bez jaj",
                                Date = DateTime.Now
                            }
                        }
                    }
                }
            }
        };
    }
}
