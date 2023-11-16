using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Incomes
{
    public class IncomeCategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
