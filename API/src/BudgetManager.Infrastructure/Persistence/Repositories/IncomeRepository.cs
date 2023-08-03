using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    internal class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;
        public IncomeRepository(ApplicationDbContext context) => _context = context;

        public async Task CreateAsync(Income income, CancellationToken cancellationToken)
        {
            await _context.Incomes.AddAsync(income, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
        public Income? Get(Guid id) => _context.Incomes.FirstOrDefault(i => i.Id == id);

        public async Task DeleteAsync(Income income, CancellationToken cancellationToken)
        {
            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Income expense, CancellationToken cancellationToken)
        {
            _context.Incomes.Update(expense);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
