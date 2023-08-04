using BudgetManager.Domain.Incomes;

namespace BudgetManager.Application.Interfaces
{
    public interface IIncomeTableRepository
    {
        IEnumerable<IncomeCategory> Get(int year, int month);
    }
}
