using CRM_system_backend.Contracts.SupplySet;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.SupplySet;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplySetController : ControllerBase
{
    private readonly ISupplySetService _supplySetService;

    public SupplySetController(ISupplySetService supplySetService)
    {
        _supplySetService = supplySetService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplySetItem>>> GetPagetSupplySets([FromQuery] SupplySetFilter filter)
    {
        var dto = await _supplySetService.GetPagetSupplySets(filter);
        var count = await _supplySetService.GetCountSupplySets(filter);

        var response = dto.Select(s => new SupplySet(
            s.Id,
            s.SupplyId,
            s.PositionId,
            s.Quantity,
            s.PurchasePrice));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateSupplySet([FromBody] SupplySetRequest request)
    {
        var (supplySet, errors) = SupplySet.Create(
            0,
            request.SupplyId,
            request.PositionId,
            request.Quantity,
            request.PurchasePrice);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplySetService.CreateSupplySet(supplySet!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateSupplySet(long id, [FromBody] SupplySetUpdateRequest request)
    {
        var model = new SupplySetUpdateModel(
            request.Quantity,
            request.PurchasePrice);

        var Id = await _supplySetService.UpdateSupplySet(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteSupplySet(long id)
    {
        var Id = await _supplySetService.DeleteSupplySet(id);

        return Ok(Id);
    }
}
