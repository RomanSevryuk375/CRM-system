using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Supply;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/vi/supplies")]
[ApiController]
public class SupplyController(
    ISupplyService supplyService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplyItem>>> GetPagedSupplies(
        [FromQuery]SupplyFilter filter, CancellationToken ct)
    {
        var dto = await supplyService.GetPagedSupplies(filter, ct);
        var count = await supplyService.GetCountSupplies(filter, ct);

        var response = mapper.Map<List<SupplyResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSupply(
        SupplyRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SupplyCreateModel>(request);

        await supplyService.CreateSupply(createModel, ct);

        return Created();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSupply(long id, CancellationToken ct)
    {
        await supplyService.DeleteSupply(id, ct);

        return NoContent();
    }
}
