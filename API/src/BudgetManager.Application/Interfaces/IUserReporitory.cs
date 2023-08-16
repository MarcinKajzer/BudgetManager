using BudgetManager.Domain.Users;

namespace BudgetManager.Application.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}
