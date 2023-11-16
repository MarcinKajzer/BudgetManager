using BudgetManager.Application.Interfaces;

namespace BudgetManager.Application.Common;
public class IdStorage : IIdStorage
{
    private Guid _id;

    public void SetId(Guid id)
    {
        _id = id;
    }

    public Guid GetId()
    {
        return _id;
    }
}
