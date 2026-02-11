using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Client;
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
        ClientsRequest request, CancellationToken ct)
    {
        var (client, errors) = Client.Create(
            0,
            request.UserId,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        var clientId = await clientService.CreateClient(client!, ct);

        return CreatedAtAction(
            nameof(GetClientById), 
            new { Id = clientId }, 
            null);
    }

    [HttpPost("/user")]
    public async Task<ActionResult> CreateClientWithUser(
        [FromBody] ClientRegisterRequest request, CancellationToken ct)
    {
        var (user, errorsUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (errorsUser is not null && errorsUser.Any())
        {
            return BadRequest(errorsUser);
        }

        var (client, errorsClient) = Client.Create(
            0,
            0,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

        if (errorsClient is not null && errorsClient.Any())
        {
            return BadRequest(errorsClient);
        }

        var clientId = await clientService.CreateClientWithUser(client!, user!, ct);

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
        var model = new ClientUpdateModel(
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Email);

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
