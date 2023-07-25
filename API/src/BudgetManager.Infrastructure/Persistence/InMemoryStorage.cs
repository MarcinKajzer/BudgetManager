using BudgetManager.Domain.Expenses;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence
{
    internal class InMemoryStorage
    {
        public static List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>
        {
            new ExpenseCategory { Name = "Dom", Id = Guid.NewGuid() },
            new ExpenseCategory { Name = "Zdrowie", Id = Guid.NewGuid() },
            new ExpenseCategory { Name = "Transport", Id = Guid.NewGuid() },
            new ExpenseCategory 
            { 
                Name = "Rozwój osobisty", 
                Id = Guid.NewGuid(), 
                Subcategories = new List<ExpenseSubcategory>
                {
                    new ExpenseSubcategory
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

        public static List<IncomeCategory> incomeCategories = new List<IncomeCategory>
        {
            new IncomeCategory { Name = "Freelance", Id = Guid.NewGuid() },
            new IncomeCategory
            {
                Name = "Praca UoP",
                Id = Guid.NewGuid(),
                Incomes = new List<Income>
                {
                   new Income
                   {
                        Id = Guid.NewGuid(),
                        Amount = 6200.14m,
                        Comment = "Wynagrodzenie za pracę",
                        Date = DateTime.Now
                   }
                }
            }
        };
    }
}
