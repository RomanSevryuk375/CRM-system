using AutoMapper;
using CRMSystem.Business.Abstractions;
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
    public async Task<ActionResult<List<ClientsResponse>>> GetPagedClient(
        [FromQuery] ClientFilter filter, CancellationToken ct)
    {
        var dto = await clientService.GetPagedClients(filter, ct);
        var count = await clientService.GetCountClients(filter, ct);

        var response = mapper.Map<List<ClientsResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<ClientsResponse>> GetClientById(
        long id, CancellationToken ct)
    {
        var dto = await clientService.GetClientById(id, ct);
        var response = mapper.Map<ClientsResponse>(dto);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateClient(
        ClientRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<ClientCreateModel>(request);
        var id = await clientService.CreateClient(createModel, ct);
        
        var createdDto = await clientService.GetClientById(id, ct);
        var response = mapper.Map<ClientsResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetClientById), 
            new { id = createdDto.Id }, 
            response);
    }

    [HttpPost("users")]
    public async Task<ActionResult> CreateClientWithUser(
        [FromBody] ClientRegisterRequest request, CancellationToken ct)
    {
        var clientCreateModel = mapper.Map<ClientCreateModel>(request);
        var userCreateModel = mapper.Map<UserCreateModel>(request);
        var id = await clientService.CreateClientWithUser(clientCreateModel!, userCreateModel!, ct);
        
        var createdDto = await clientService.GetClientById(id, ct);
        var response = mapper.Map<ClientsResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetClientById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> UpdateClient(
        long id, [FromBody] ClientUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<ClientUpdateModel>(request);
        await clientService.UpdateClient(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> DeleteClient(
        long id, CancellationToken ct)
    {
        await clientService.DeleteClient(id, ct);

        return NoContent();
    }
}
