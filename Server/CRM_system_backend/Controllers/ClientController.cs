using CRM_system_backend.Contracts;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientsResponse>>> GetClient()
    {
        var clients = await _clientService.GetClients();

        var response = clients
            .Select(b => new ClientsResponse(b.Id, b.UserId, b.Name, b.Surname, b.Email, b.PhoneNumber))
            .ToList(); 

        return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult<int>> CreateClient([FromBody] ClientsRequest request)
    {
        var (client, error) = Client.Create(
            0,
            request.UserId,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var clientId = await _clientService.CreateClient(client);

        return Ok(clientId);
    }

    [HttpPut]

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

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteUser(int id)
    {
        var result = await _clientService.DeleteClient(id);

        return Ok(result);
    }
}
