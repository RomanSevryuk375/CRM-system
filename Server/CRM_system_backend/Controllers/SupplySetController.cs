using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.SupplySet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.SupplySet;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/supply-sets")]
public class SupplySetController(
    ISupplySetService supplySetService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplySetResponse>>> GetPagedSupplySets(
        [FromQuery] SupplySetFilter filter, CancellationToken ct)
    {
        var dto = await supplySetService.GetPagedSupplySets(filter, ct);
        var count = await supplySetService.GetCountSupplySets(filter, ct);

        var response = mapper.Map<List<SupplySetResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<SupplySetResponse>> GetSupplySetById(
        long id, CancellationToken ct)
    {
        var dto = await supplySetService.GetSupplySetById(id, ct);
        var response = mapper.Map<SupplySetResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateSupplySet(
        [FromBody] SupplySetRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SupplySetCreateModel>(request);
        var id = await supplySetService.CreateSupplySet(createModel, ct);
        
        var createdDto = await supplySetService.GetSupplySetById(id, ct);
        var response = mapper.Map<SupplySetResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetSupplySetById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSupplySet(
        long id, [FromBody] SupplySetUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<SupplySetUpdateModel>(request);
        await supplySetService.UpdateSupplySet(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSupplySet(long id, CancellationToken ct)
    {
        await supplySetService.DeleteSupplySet(id, ct);

        return NoContent();
    }
}
