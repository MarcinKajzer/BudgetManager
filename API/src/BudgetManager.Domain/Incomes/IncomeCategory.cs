using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Incomes
{
    public class IncomeCategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
