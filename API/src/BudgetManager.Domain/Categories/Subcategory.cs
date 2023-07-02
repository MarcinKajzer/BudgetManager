using BudgetManager.Domain.Expenses;

namespace BudgetManager.Domain.Categories
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
