using System.Security.Claims;

namespace CRM.Shared.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        string? id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Guid.TryParse(id, out Guid guid)
            ? guid
            : Guid.Empty;
    }
}