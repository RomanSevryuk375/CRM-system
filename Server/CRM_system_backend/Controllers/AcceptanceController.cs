using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Acceptance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Acceptance;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/acceptances")]

public class AcceptanceController(
    IAcceptanceService acceptanceService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AcceptanceItem>>> GetPagedAcceptance(
        [FromQuery] AcceptanceFilter filter, CancellationToken ct)
    {
        var dto = await acceptanceService.GetPagedAcceptance(filter, ct);
        var count = await acceptanceService.GetCountAcceptance(filter, ct);

        var response = mapper.Map<List<AcceptanceResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAcceptance(
        [FromBody] AcceptanceRequest request, CancellationToken ct)
    {
        var (acceptance, errors) = Acceptance.Create(
            0,
            request.OrderId,
            request.WorkerId,
            request.CreatedAt,
            request.Mileage,
            request.FuelLevel,
            request.ExternalDefects,
            request.InternalDefects,
            request.ClientSign,
            request.WorkerSign);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await acceptanceService.CreateAcceptance(acceptance!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAcceptance(
        long id, [FromBody] AcceptanceUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<AcceptanceUpdateModel>(request);

        await acceptanceService.UpdateAcceptance(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAcceptance(long id, CancellationToken ct)
    {
        await acceptanceService.DeleteAcceptance(id, ct);

        return NoContent();
    }
}
