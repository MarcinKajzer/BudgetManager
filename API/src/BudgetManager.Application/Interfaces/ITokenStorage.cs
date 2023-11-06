namespace BudgetManager.Application.Interfaces;
public interface ITokenStorage
{
    string GetToken();
    void SetToken(string token);
}
