using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Login;
using Shared.Contracts.User;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] LoginRequest loginRequest, CancellationToken ct)
    {
        var token = await _userService.LoginUser(loginRequest.Login, loginRequest.Password, ct);
        var user = await _userService.GetUsersByLogin(loginRequest.Login, ct);

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
        var user = await _userService.GetUsersByLogin(login, ct);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateUser([FromBody] UserRequest request, CancellationToken ct)
    {
        var (user, errors) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);


        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var userId = await _userService.CreateUser(user!, ct);

        return userId;
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteUser(int id, CancellationToken ct)
    {
        var result = await _userService.DeleteUser(id, ct);

        return Ok(id);
    }
}
