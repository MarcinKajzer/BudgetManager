using BudgetManager.Domain.Incomes;

namespace BudgetManager.Domain.EarningsCategories
{
    public class IncomeCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
    }
}
