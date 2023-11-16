using BudgetManager.Application.Auth.Models;

namespace BudgetManager.Application.Interfaces;
public interface IIdStorage
{
    void SetId(Guid id);
    Guid GetId();
}
