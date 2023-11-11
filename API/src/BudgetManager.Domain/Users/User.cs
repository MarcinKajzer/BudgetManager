using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Users;

public class User : BaseAuditableEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshTokenExpirationDate { get; set; }
}
