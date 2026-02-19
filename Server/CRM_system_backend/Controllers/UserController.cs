using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Login;
using Shared.Contracts.User;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginUser(
        [FromBody] LoginRequest loginRequest, CancellationToken ct)
    {
        var token = await userService.LoginUser(loginRequest.Login, loginRequest.Password, ct);
        var user = await userService.GetUsersByLogin(loginRequest.Login, ct);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(12),
            Path = "/",
            IsEssential = true
        };

        Response.Cookies.Append("jwt", token, cookieOptions);
        var roleId = user.RoleId;

        var response = new LoginResponse
        {
            Token = token,
            RoleId = roleId,
            Message = "Logged in"
        };

        return Ok(response);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {

        Response.Cookies.Delete("jwt", new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Path = "/"
        });

        return Ok(new { Message = "Logged out" });
    }

    [HttpGet("by-login/{login}")]
    public async Task<ActionResult<UserItem>> GetUserByLogin(string login, CancellationToken ct)
    {
        var user = await userService.GetUsersByLogin(login, ct);
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] UserRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<UserCreateModel>(request);

        await userService.CreateUser(createModel, ct);

        return Created();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteUser(int id, CancellationToken ct)
    {
        await userService.DeleteUser(id, ct);

        return NoContent();
    }
}
