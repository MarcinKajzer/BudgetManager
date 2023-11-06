using BudgetManager.Application.Interfaces;

namespace BudgetManager.Infrastructure.Security;
public class TokenStorage : ITokenStorage
{
    private string _token;

    //Probably token will be saved in diferent place, so get and set is used
    public string GetToken()
    {
        return _token;
    }

    public void SetToken(string token)
    {
        _token = token;
    }
}
