using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Expenses
{
    public class Expense : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual ExpenseSubcategory Subcategory { get; set; }
    }
}
