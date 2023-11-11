using BudgetManager.Application.Auth.Models;
using BudgetManager.Application.Interfaces;

namespace BudgetManager.Infrastructure.Security;
public class TokenStorage : ITokenStorage
{
    private TokenModel _tokens = new TokenModel();

    public void SetAccessToken(string token)
    {
        _tokens.AccessToken = token;
    }

    public void SetRefreshToken(string token)
    {
        _tokens.RefreshToken = token;
    }

    public TokenModel GetTokens() => _tokens;
}