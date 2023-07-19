using BudgetManager.Domain.EarningsCategories;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeTableRepository
    {
        IEnumerable<IncomeCategory> Get();
    }
}
