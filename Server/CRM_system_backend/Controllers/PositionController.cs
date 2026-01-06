using AutoMapper;
using CRM_system_backend.Contracts.Position;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IPositionService _positionSrevice;
    private readonly IMapper _mapper;

    public PositionController(
        IPositionService positionService,
        IMapper mapper)
    {
        _positionSrevice = positionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PositionItem>>> GetPagedPositions([FromQuery] PositionFilter positionFilter)
    {
        var dto = await _positionSrevice.GetPagedPositions(positionFilter);
        var count = await _positionSrevice.GetCountPositions(positionFilter);

        var response = _mapper.Map<List<PositionResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost("with-part")]
    public async Task<ActionResult<long>> CreatePositionWithPart([FromBody] PositionWithPartRequest request)
    {
        var (part, partErrors) = Part.Create(
            0,
            request.CategoryId,
            request.OemArticle,
            request.ManufacturerArticle,
            request.InternalArticle,
            request.Description,
            request.Name,
            request.Manufacturer,
            request.Applicability);

        if(partErrors is not null && partErrors.Any()) 
            return BadRequest(partErrors);

        var (position, positionErrors) = Position.Create(
            0,
            1,
            request.CellId,
            request.PurchasePrice,
            request.SellingPrice,
            request.Quantity);

        if (positionErrors is not null && positionErrors.Any())
            return BadRequest(positionErrors);

        var Id = await _positionSrevice.CreatePositionWithPart(position!, part!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdatePosition(long id,[FromBody] PositionUpdateRequest request)
    {
        var model = new PositionUpdateModel(
            request.CellId,
            request.PurchasePrice,
            request.PurchasePrice,
            request.Quantity);

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
