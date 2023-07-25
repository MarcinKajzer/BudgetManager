using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Incomes
{
    public class Income : BaseAuditableEntity
    {
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public virtual IncomeCategory Category { get; set; }
    }
}
