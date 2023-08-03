using BudgetManager.Application.Interfaces;

namespace BudgetManager.Infrastructure.Services;

internal class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
