using BudgetManager.Application.Security;
using Microsoft.AspNet.Identity;

namespace BudgetManager.Infrastructure.Security;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher _hasher;
    public PasswordManager(IPasswordHasher hasher) => _hasher = hasher;
    

    public string Hash(string password) => _hasher.HashPassword(password);
    
    public bool Validate(string password, string posswordHash)
        => _hasher.VerifyHashedPassword(password, posswordHash) == PasswordVerificationResult.Success;
    
}
