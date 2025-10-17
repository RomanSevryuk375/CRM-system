using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]

    public async Task<ActionResult<List<UserResponse>>> GetUser()
    {
        var users = await _userService.GetUsers();

        var response = users.Select(u => new UserResponse(u.Id, u.RoleId, u.Login, u.PasswordHash));

        return Ok(users);
    }

    [HttpPost]

    public async Task<ActionResult<int>> CreateUser([FromBody] UserRequest request)
    {
        var (user, error) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.PasswordHash
        );

        var userId = await _userService.CreateUser(user);

        return Ok(userId);
    }
}
