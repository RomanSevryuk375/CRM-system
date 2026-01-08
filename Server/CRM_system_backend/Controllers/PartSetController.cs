using AutoMapper;
using CRM_system_backend.Contracts.PartSet;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartSetController : ControllerBase
{
    private readonly IPartSetService _partSetService;
    private readonly IMapper _mapper;

    public PartSetController(
        IPartSetService partSetService,
        IMapper mapper)
    {
        _partSetService = partSetService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PartSetItem>>> GetPagedPartSets([FromQuery] PartSetFilter filter, CancellationToken ct)
    {
        var dto = await _partSetService.GetPagedPartSets(filter, ct);
        var count = await _partSetService.GetCountPartSets(filter, ct);

        var response = _mapper.Map<List<PartSetResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartSetItem>> GetPartSetById(long id, CancellationToken ct)
    {
        var response = await _partSetService.GetPartSetById(id, ct);

        return Ok(response);
    }

    [HttpGet("/order/{orderId}")]
    public async Task<ActionResult<List<PartSetItem>>> GetPartSetsByOrderId(long orderId, CancellationToken ct)
    {
        var dto = await _partSetService.GetPartSetsByOrderId(orderId, ct);

        var response = _mapper.Map<List<PartSetResponse>>(dto);

        return Ok(response);
    }

    [HttpPost] 
    public async Task<ActionResult<long>> AddToPartSet([FromBody] PartSetRequest request, CancellationToken ct)
    {
        var (partSet, errors) = PartSet.Create(
            0,
            request.OrderId,
            request.PositionId,
            request.ProposalId,
            request.Quantity,
            request.SoldPrice);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _partSetService.AddToPartSet(partSet!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdatePartSet(long id, [FromBody] PartSetUpdateRequest request, CancellationToken ct)
    {
        var model = new PartSetUpdateModel(
            request.Quantity,
            request.SoldPrice);

        var Id = await _partSetService.UpdatePartSet(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteFromPartSet(long id, CancellationToken ct)
    {
        var Id = await _partSetService.DeleteFromPartSet(id, ct);

        return Ok(Id);
    }
}
