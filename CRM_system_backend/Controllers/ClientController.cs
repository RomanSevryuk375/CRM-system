using CRM_system_backend.Contracts;
using Microsoft.AspNetCore.Mvc;
using system.Buisnes.Services;
using system.Core.Models;

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

        var response = clients.Select(b => new ClientsResponse(b.Id, b.UserId, b.Name, b.Surname, b.Email, b.PhoneNumber));

        return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult<int>> CreateClient([FromBody] ClientsRequest request)
    {
        var (client, error) = Client.Create(
            0,
            request.UserId,    // Добавлен UserId из запроса
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var clientId = await _clientService.CeateClient(client);

        return Ok(clientId);
    }
}
