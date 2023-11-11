using System.Security.Claims;

namespace BudgetManager.Application.Security;
public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    string? GetUserIdFromAccessToken(string token);
}
