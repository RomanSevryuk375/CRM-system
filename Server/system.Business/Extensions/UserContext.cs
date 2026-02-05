using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CRMSystem.Business.Extensions;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public long UserId => 
        long.Parse(accessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

    public int RoleId => 
        int.Parse(accessor.HttpContext?.User?.FindFirst("userRoleId")?.Value ?? "0");

    public long ProfileId => 
        long.Parse(accessor.HttpContext?.User?.FindFirst("profileId")?.Value ?? "0");
}
