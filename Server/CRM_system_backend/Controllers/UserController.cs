using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
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
        var result = await _userService.LoginUser(loginRequest.Login, loginRequest.Password);
        return Ok(new { Message = result });
    }

    [HttpGet("by-login/{login}")]
    public async Task<ActionResult<User>> GetUserByLogin(string login)
    {
            var user = await _userService.GetUsersByLogin(login);
            return Ok(user);
    }

    [HttpPost]

    public async Task<ActionResult<int>> CreateUser([FromBody] UserRequest request)
    {
        var (user, error) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);


        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var userId = await _userService.CreateUser(user);

        return Ok(userId);
    }
}
