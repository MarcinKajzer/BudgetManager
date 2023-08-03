using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeCategoryRepository(ApplicationDbContext context) => _context = context;

        public async Task CreateAsync(IncomeCategory category, CancellationToken cancellationToken)
        {
            await _context.IncomeCategories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IncomeCategory? Get(Guid id)
        {
            return _context.IncomeCategories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<IncomeCategory> GetAll() => _context.IncomeCategories;

        public async Task UpdateAsync(IncomeCategory category, CancellationToken cancellationToken)
        {
            _context.IncomeCategories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(IncomeCategory category, CancellationToken cancellationToken)
        {
            _context.IncomeCategories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
