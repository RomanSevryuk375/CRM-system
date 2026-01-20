using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CRMSystem.Business.Extensions;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _accessor;

    public UserContext(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public long UserId => long.Parse(_accessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

    public int RoleId => int.Parse(_accessor.HttpContext?.User?.FindFirst("userRoleId")?.Value ?? "0");

    public long ProfileId => long.Parse(_accessor.HttpContext?.User?.FindFirst("profileId")?.Value ?? "0");
}
