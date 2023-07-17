using BudgetManager.Domain.Expenses;

namespace BudgetManager.Domain.Categories
{
    public class ExpenseSubcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
