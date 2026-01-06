using CRM_system_backend.Contracts;
using CRM_system_backend.Contracts.User;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginUser([FromBody] LoginRequest loginRequest)
    {
        var token = await _userService.LoginUser(loginRequest.Login, loginRequest.Password);
        var user = await _userService.GetUsersByLogin(loginRequest.Login);

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
        var userId = user.RoleId;

        return Ok(new { Message = "Logged in", Token = token , RoleId = userId});
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
    public async Task<ActionResult<UserItem>> GetUserByLogin(string login)
    {
        var user = await _userService.GetUsersByLogin(login);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateUser([FromBody] UserRequest request)
    {
        var (user, errors) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);


        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var userId = await _userService.CreateUser(user!);

        return userId;
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteUser (int id)
    {
        var result = await _userService.DeleteUser(id);

        return Ok(id);
    }
}
