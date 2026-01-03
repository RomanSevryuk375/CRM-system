using CRM_system_backend.Contracts.PartSet;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartSetController : ControllerBase
{
    private readonly IPartSetService _partSetService;

    public PartSetController(IPartSetService partSetService)
    {
        _partSetService = partSetService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PartSetItem>>> GetPagedPartSets([FromQuery] PartSetFilter filter)
    {
        var dto = await _partSetService.GetPagedPartSets(filter);
        var count = await _partSetService.GetCountPartSets(filter);

        var response = dto.Select(p => new PartSetResponse(
            p.Id,
            p.OrderId,
            p.Position,
            p.PositionId,
            p.ProposalId,
            p.Quantity,
            p.SoldPrice));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartSetItem>> GetPartSetById(long id)
    {
        var response = await _partSetService.GetPartSetById(id);

        return Ok(response);
    }

    [HttpGet("/order/{orderId}")]
    public async Task<ActionResult<List<PartSetItem>>> GetPartSetsByOrderId(long orderId)
    {
        var dto = await _partSetService.GetPartSetsByOrderId(orderId);

        var response = dto.Select(p => new PartSetResponse(
            p.Id,
            p.OrderId,
            p.Position,
            p.PositionId,
            p.ProposalId,
            p.Quantity,
            p.SoldPrice));

        return Ok(response);
    }

    [HttpPost] 
    public async Task<ActionResult<long>> AddToPartSet([FromBody] PartSetRequest request)
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

        var Id = await _partSetService.AddToPartSet(partSet!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdatePartSet(long id, [FromBody] PartSetUpdateRequest request)
    {
        var model = new PartSetUpdateModel(
            request.Quantity,
            request.SoldPrice);

        var Id = await _partSetService.UpdatePartSet(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteFromPartSet(long id)
    {
        var Id = await _partSetService.DeleteFromPartSet(id);

        return Ok(Id);
    }
}
