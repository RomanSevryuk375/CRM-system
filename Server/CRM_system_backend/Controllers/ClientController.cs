using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IUserService _userService;

    public ClientController(IClientService clientService, IUserService userService)
    {
        _clientService = clientService;
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<ClientsResponse>>> GetClient()
    {
        var clients = await _clientService.GetClients();

        var response = clients
            .Select(b => new ClientsResponse(b.Id, b.UserId, b.Name, b.Surname, b.Email, b.PhoneNumber))
            .ToList(); 

        return Ok(response);
    }

    [HttpGet("My")]
    [Authorize(Policy = "UserPolicy")]

    public async Task<ActionResult<List<Client>>> GetClientByUserId()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var clients = await _clientService.GetClientByUserId(userId);

        var response = clients
            .Select(b => new ClientsResponse(b.Id, b.UserId, b.Name, b.Surname, b.Email, b.PhoneNumber))
            .ToList();

        return Ok(response);
    }

    [HttpPost("with create user")]

    public async Task<ActionResult<int>> CreateClient([FromBody] ClientRegistreRequest request)
    {
        var (user, errorUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (!string.IsNullOrEmpty(errorUser))
            return BadRequest(errorUser);

        var userId = await _userService.CreateUser(user);

        var (client, error) = Client.Create(
            0,
            userId,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (!string.IsNullOrEmpty(error))
        {
            await _userService.DeleteUser(userId);
            return BadRequest(error);
        }

        var clientId = await _clientService.CreateClient(client);

        return Ok(new
        {
            Message = "Registration successful",
            UserId = userId,
            ClientId = clientId
        });
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdateClient([FromBody] ClientUpdateRequest clientUpdateRequest, int id)
    {
        var result = await _clientService.UpdateClient(
                    id,
                    clientUpdateRequest.Name,
                    clientUpdateRequest.Surname,
                    clientUpdateRequest.PhoneNumber,
                    clientUpdateRequest.Email);

            return Ok(result);
    }

    //user get it by id from jwt
}
