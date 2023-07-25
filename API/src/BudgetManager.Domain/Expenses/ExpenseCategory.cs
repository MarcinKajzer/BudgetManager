using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Expenses
{
    public class ExpenseCategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ExpenseSubcategory> Subcategories { get; set;}
    }
}
