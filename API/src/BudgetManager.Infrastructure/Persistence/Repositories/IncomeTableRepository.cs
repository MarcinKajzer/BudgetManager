using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class IncomeTableRepository : IIncomeTableRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeTableRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<IncomeCategory> Get() => _context.IncomeCategories;
    }
}
