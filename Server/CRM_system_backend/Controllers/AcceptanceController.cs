using AutoMapper;
using CRM_system_backend.Contracts.Acceptance;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<AcceptanceItem>>> GetPagedAcceptance([FromQuery] AcceptanceFilter filter)
    {
        var dto = await _acceptanceService.GetPagedAcceptance(filter);
        var count = await _acceptanceService.GetCountAcceptance(filter);

        var response = _mapper.Map<List<AcceptanceResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAcceptance([FromBody] AcceptanceRequest request)
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

        var Id = await _acceptanceService.CreateAcceptance(acceptance!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAcceptance(long id, [FromBody] AcceptanceUpdateRequest request)
    {
        var model = _mapper.Map<AcceptanceUpdateModel>(request);

        var Id = await _acceptanceService.UpdateAcceptance(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAcceptance(long id)
    {
        var Id = await _acceptanceService.DeleteAcceptance(id);

        return Ok(Id);
    }
}
