using ProjectTaskManagement.Application.Interfaces;
using System.Security.Claims;

namespace ProjectTaskManagement.Api.Extensions;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new UnauthorizedAccessException("User is not authenticated.");
}
