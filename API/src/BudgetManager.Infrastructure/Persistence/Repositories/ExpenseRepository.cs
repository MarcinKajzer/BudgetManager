using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context) => _context = context;

        public async Task CreateAsync(Expense expense, CancellationToken cancellationToken)
        {
            await _context.Expenses.AddAsync(expense, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Expense?> GetAsync(Guid id)
        {
            return _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task DeleteAsync(Expense expense, CancellationToken cancellationToken)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Expense expense, CancellationToken cancellationToken)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
