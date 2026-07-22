using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace CRM.Shared.Infrastructure;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private static readonly Guid SystemUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    public Guid UserId
    {
        get
        {
            HttpContext? context = httpContextAccessor.HttpContext;
            return context is null
                ? SystemUserId
                : context.User.GetUserId();
        }
    }

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}