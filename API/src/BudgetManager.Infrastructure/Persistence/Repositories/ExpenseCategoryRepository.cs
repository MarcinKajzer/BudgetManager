using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ExpenseCategory category, CancellationToken cancellationToken)
        {
            await _context.ExpenseCategory.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public ExpenseCategory? Get(Guid id)
        {
            return _context.ExpenseCategory.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ExpenseCategory> GetAll() => _context.ExpenseCategory.Include(c => c.Subcategories);

        public async Task UpdateAsync(ExpenseCategory category, CancellationToken cancellationToken)
        {
            _context.ExpenseCategory.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ExpenseCategory category, CancellationToken cancellationToken)
        {
            _context.ExpenseCategory.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
