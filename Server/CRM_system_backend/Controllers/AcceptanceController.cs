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
[Route("api/[controller]")]

public class AcceptanceController : ControllerBase
{
    private readonly IAcceptanceService _acceptanceService;
    private readonly IMapper _mapper;

    public AcceptanceController(
        IAcceptanceService acceptanceService,
        IMapper mapper)
    {
        _acceptanceService = acceptanceService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AcceptanceItem>>> GetPagedAcceptance([FromQuery] AcceptanceFilter filter, CancellationToken ct)
    {
        var dto = await _acceptanceService.GetPagedAcceptance(filter, ct);
        var count = await _acceptanceService.GetCountAcceptance(filter, ct);

        var response = _mapper.Map<List<AcceptanceResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> CreateAcceptance([FromBody] AcceptanceRequest request, CancellationToken ct)
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

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _acceptanceService.CreateAcceptance(acceptance!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> UpdateAcceptance(long id, [FromBody] AcceptanceUpdateRequest request, CancellationToken ct)
    {
        var model = _mapper.Map<AcceptanceUpdateModel>(request);

        var Id = await _acceptanceService.UpdateAcceptance(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> DeleteAcceptance(long id, CancellationToken ct)
    {
        var Id = await _acceptanceService.DeleteAcceptance(id, ct);

        return Ok(Id);
    }
}
