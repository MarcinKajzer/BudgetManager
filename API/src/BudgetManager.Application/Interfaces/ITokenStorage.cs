using BudgetManager.Application.Auth.Models;

namespace BudgetManager.Application.Interfaces;
public interface ITokenStorage
{
    void SetAccessToken(string token);
    void SetRefreshToken(string token);
    TokenModel GetTokens();
}