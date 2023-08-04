using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseTableRepository : IExpenseTableRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseTableRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<ExpenseCategory> Get(int year, int month) 
            => _context.ExpenseCategory.Include(e => e.Subcategories).ThenInclude(s => s.Expenses.Where(e => e.Date.Year == year && e.Date.Month == month));
    }
}
