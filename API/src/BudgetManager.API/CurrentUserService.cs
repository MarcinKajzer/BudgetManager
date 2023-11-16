using BudgetManager.Application.Interfaces;
using System.Security.Claims;

namespace BudgetManager.API;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _contextAccessor = httpContextAccessor;

    public string? UserId => _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
