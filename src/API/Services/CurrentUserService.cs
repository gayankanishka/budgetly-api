using System.Security.Claims;
using Budgetly.Application.Common.Interfaces;

namespace Budgetly.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    // TODO: GK | remove hardcoded userId
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) 
                             ?? "auth0|61d9c13ff98384007046a028";
}