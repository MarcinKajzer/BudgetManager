namespace BudgetManager.Application.Security;

public interface IPasswordManager
{
    string Hash(string password);
    bool Validate(string password, string posswordHash);
}
