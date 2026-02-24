using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Client;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/clients")]
public class ClientController(
    IClientService clientService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<ClientItem>>> GetPagedClient(
        [FromQuery] ClientFilter filter, CancellationToken ct)
    {
        var dto = await clientService.GetPagedClients(filter, ct);
        var count = await clientService.GetCountClients(filter, ct);

        var response = mapper.Map<List<ClientsResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<Client>>> GetClientById(
        long id, CancellationToken ct)
    {
        var response = await clientService.GetClientById(id, ct);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateClient(
        ClientRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<ClientCreateModel>(request);

        var clientId = await clientService.CreateClient(createModel, ct);

        return CreatedAtAction(
            nameof(GetClientById), 
            new { Id = clientId }, 
            null);
    }

    [HttpPost("/user")]
    public async Task<ActionResult> CreateClientWithUser(
        [FromBody] ClientRegisterRequest request, CancellationToken ct)
    {
        var clientCreateModel = mapper.Map<ClientCreateModel>(request);
        var userCreateModel = mapper.Map<UserCreateModel>(request);

        var clientId = await clientService.CreateClientWithUser(clientCreateModel!, userCreateModel!, ct);

        return CreatedAtAction(
            nameof(GetClientById),
            new { Id = clientId },
            null);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> UpdateClient(
        long id, [FromBody] ClientUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<ClientUpdateModel>(request);

        await clientService.UpdateClient(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> DeleteClient(
        long id, CancellationToken ct)
    {
        await clientService.DeleteClient(id, ct);

        return NoContent();
    }
}
