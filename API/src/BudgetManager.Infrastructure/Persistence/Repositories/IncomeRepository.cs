using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Microsoft.EntityFrameworkCore;

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
        public Task<Income?> GetAsync(Guid id, CancellationToken cancellationToken) => _context.Incomes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

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
