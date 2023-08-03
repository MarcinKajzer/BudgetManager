using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseTableRepository : IExpenseTableRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseTableRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<ExpenseCategory> Get() => _context.ExpenseCategory;
    }
}
