using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Client;

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
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<ClientItem>>> GetPagedClient([FromQuery] ClientFilter filter, CancellationToken ct)
    {
        var dto = await _clientService.GetPagedClients(filter, ct);
        var count = await _clientService.GetCountClients(filter, ct);

        var response = _mapper.Map<List<ClientsResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<Client>>> GetClientById(long id, CancellationToken ct)
    {
        var response = await _clientService.GetClientById(id, ct);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateClient(ClientsRequest request, CancellationToken ct)
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

        var Id = await _clientService.CreateClient(client!, ct);

        return Ok(Id);
    }

    [HttpPost("with-user")]
    public async Task<ActionResult<int>> CreateClientWithUser([FromBody] ClientRegisterRequest request, CancellationToken ct)
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

        var clientId = await _clientService.CreateClientWithUser(client!, user!, ct);

        return Ok(clientId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<long>> UpdateClient(long id, [FromBody] ClientUpdateRequest request, CancellationToken ct)
    {
        var model = new ClientUpdateModel(
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        var Id = await _clientService.UpdateClient(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<long>> DeleteClient(long id, CancellationToken ct)
    {
         var Id = await _clientService.DeleteClient(id, ct);

        return Ok(Id);
    }
}
