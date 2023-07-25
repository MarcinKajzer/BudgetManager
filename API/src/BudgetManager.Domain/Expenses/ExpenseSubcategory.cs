using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Expenses
{
    public class ExpenseSubcategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
