using CRM_system_backend.Contracts.Supply;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Supply;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplyController : ControllerBase
{
    private readonly ISupplyService _supplyService;

    public SupplyController(ISupplyService supplyService)
    {
        _supplyService = supplyService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplyItem>>> GetPagedSupplies([FromQuery]SupplyFilter filter)
    {
        var dto = await _supplyService.GetPagedSupplies(filter);
        var count = await _supplyService.GetCountSupplies(filter);

        var response = dto.Select(s => new SupplyResponse(
            s.id,
            s.supplier,
            s.supplierId,
            s.date));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateSupply(SupplyRequest request)
    {
        var (supply, errors) = Supply.Create(
            0,
            request.supplierId,
            request.date);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplyService.CreateSupply(supply!);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteSupply(long id)
    {
        var Id = await _supplyService.DeleteSupply(id);

        return Ok(Id);
    }
}
