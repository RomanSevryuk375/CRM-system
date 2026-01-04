using AutoMapper;
using CRM_system_backend.Contracts.Client;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(
        IClientService clientService,
        IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientItem>>> GetPagedClient([FromQuery] ClientFilter filter)
    {
        var dto = await _clientService.GetPagedCkients(filter);
        var count = await _clientService.GetCountClients(filter);

        var response = _mapper.Map<List<ClientsResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Client>>> GetClientById(long id)
    {
        var response = await _clientService.GetClientById(id);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateClient(ClientsRequest request)
    {
        var (client, errors) = Client.Create(
            0,
            request.UserId,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _clientService.CreateClient(client!);

        return Ok(Id);
    }

    [HttpPost("with-user")]
    public async Task<ActionResult<int>> CreateClientWithUser([FromBody] ClientRegistreRequest request)
    {
        var (user, errorsUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (errorsUser is not null && errorsUser.Any())
            return BadRequest(errorsUser);

        var (client, errorsClient) = Client.Create(
            0,
            0,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (errorsClient is not null && errorsClient.Any())
            return BadRequest(errorsClient);

        var clientId = await _clientService.CreateClientWithUser(client!, user!);

        return Ok(clientId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateClient(long id, [FromBody] ClientUpdateRequest request)
    {
        var model = new ClientUpdateModel(
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        var Id = await _clientService.UpdateClient(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteClient(long id)
    {
         var Id = await _clientService.DeleteClient(id);

        return Ok(Id);
    }
}
