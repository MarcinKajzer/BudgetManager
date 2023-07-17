namespace BudgetManager.Domain.Categories
{
    public class ExpenseCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExpenseSubcategory> Subcategories { get; set;} = new List<ExpenseSubcategory>();
    }
}
