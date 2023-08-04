using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class IncomeTableRepository : IIncomeTableRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeTableRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<IncomeCategory> Get(int year, int month) => 
            _context.IncomeCategories.Include(c => c.Incomes.Where(i => i.Date.Year == year && i.Date.Month == month));
    }
}
