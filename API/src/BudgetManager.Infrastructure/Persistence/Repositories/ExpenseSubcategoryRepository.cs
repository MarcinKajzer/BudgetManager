using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseSubcategoryRepository : IExpenseSubcategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseSubcategoryRepository(ApplicationDbContext context) => _context = context;

        public async Task CreateAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken)
        {
            await _context.ExpenseSubcategory.AddAsync(subcategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public ExpenseSubcategory? Get(Guid id) => _context.ExpenseSubcategory.FirstOrDefault(s => s.Id == id);

        public async Task DeleteAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken)
        {
            _context.ExpenseSubcategory.Remove(subcategory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ExpenseSubcategory subcategory, CancellationToken cancellationToken)
        {
            _context.ExpenseSubcategory.Update(subcategory);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
