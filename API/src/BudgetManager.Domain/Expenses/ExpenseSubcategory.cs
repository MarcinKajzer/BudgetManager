using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Expenses
{
    public class ExpenseSubcategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public virtual ExpenseCategory Category { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
