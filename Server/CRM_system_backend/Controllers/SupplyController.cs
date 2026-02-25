using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Supply;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/supplies")]
public class SupplyController(
    ISupplyService supplyService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplyResponse>>> GetPagedSupplies(
        [FromQuery]SupplyFilter filter, CancellationToken ct)
    {
        var dto = await supplyService.GetPagedSupplies(filter, ct);
        var count = await supplyService.GetCountSupplies(filter, ct);

        var response = mapper.Map<List<SupplyResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<SupplyResponse>> GetPagedSupplyById(
        long id, CancellationToken ct)
    {
        var dto = await supplyService.GetSupplyById(id, ct);
        var response = mapper.Map<SupplyResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSupply(
        SupplyRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SupplyCreateModel>(request);
        var id = await supplyService.CreateSupply(createModel, ct);
        
        var createdDto = await supplyService.GetSupplyById(id, ct);
        var response = mapper.Map<SupplyResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetPagedSupplyById),
            new { id = createdDto.Id },
            response);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSupply(long id, CancellationToken ct)
    {
        await supplyService.DeleteSupply(id, ct);

        return NoContent();
    }
}
