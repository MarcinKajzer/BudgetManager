using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BudgetManager.Infrastructure.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTime;
    public AuditableEntitySaveChangesInterceptor(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
       CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return new ValueTask<InterceptionResult<int>>(result);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return result;
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (EntityEntry<BaseAuditableEntity> entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = Guid.NewGuid(); //Chwilowo nie ma jeszcze userów
                entry.Entity.CreatedAt = _dateTime.Now;
            }
             
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = Guid.NewGuid(); //Chwilowo nie ma jeszcze userów
                entry.Entity.LastModifiedAt = _dateTime.Now;
            }
        }
    }
}
