using CRM_system_backend.Contracts.Part;
using CRM_system_backend.Contracts.Position;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Position;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IPositionSrevice _positionSrevice;

    public PositionController(IPositionSrevice positionSrevice)
    {
        _positionSrevice = positionSrevice;
    }

    [HttpGet]
    public async Task<ActionResult<List<PositionItem>>> GetGagedPositions([FromQuery]PositionFilter positionFilter)
    {
        var dto = await _positionSrevice.GetGagedPositions(positionFilter);
        var count = await _positionSrevice.GetCountPositions(positionFilter);

        var response = dto.Select(p => new PositionResponse(
            p.id,
            p.part,
            p.partId,
            p.cellId,
            p.purchasePrice,
            p.sellingPrice,
            p.quantity));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost("with-part")]
    public async Task<ActionResult<long>> CreatePositionWithPart([FromBody] PositionWithPartRequest request)
    {
        var (part, partErrors) = Part.Create(
            0,
            request.categoryId,
            request.oemArticle,
            request.manufacturerArticle,
            request.internalArticle,
            request.description,
            request.name,
            request.manufacturer,
            request.applicability);

        if(partErrors is not null && partErrors.Any()) 
            return BadRequest(partErrors);

        var (position, positionErrors) = Position.Create(
            0,
            1,
            request.cellId,
            request.purchasePrice,
            request.sellingPrice,
            request.quantity);

        if(positionErrors is not null && positionErrors.Any()) 
            return BadRequest(positionErrors);

        var Id = await _positionSrevice.CreatePositionWithPart(position!, part!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdatePosition(long id,[FromBody] PositionUpdateRequest request)
    {
        var model = new PositionUpdateModel(
            request.cellId,
            request.purchasePrice,
            request.purchasePrice,
            request.quantity);

        var Id = await _positionSrevice.UpdatePosition(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeletePosition(long id)
    {
        var Id = await _positionSrevice.DeletePosition(id);

        return Ok(Id);
    }
}
