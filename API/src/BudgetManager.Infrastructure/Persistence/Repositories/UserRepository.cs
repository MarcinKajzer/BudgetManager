using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken) => _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) => _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
