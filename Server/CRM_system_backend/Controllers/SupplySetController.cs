using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.SupplySet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.SupplySet;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplySetController : ControllerBase
{
    private readonly ISupplySetService _supplySetService;
    private readonly IMapper _mapper;

    public SupplySetController(
        ISupplySetService supplySetService,
        IMapper mapper)
    {
        _supplySetService = supplySetService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplySetItem>>> GetPagedSupplySets([FromQuery] SupplySetFilter filter, CancellationToken ct)
    {
        var dto = await _supplySetService.GetPagedSupplySets(filter, ct);
        var count = await _supplySetService.GetCountSupplySets(filter, ct);

        var response = _mapper.Map<List<SupplySetResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateSupplySet([FromBody] SupplySetRequest request, CancellationToken ct)
    {
        var (supplySet, errors) = SupplySet.Create(
            0,
            request.SupplyId,
            request.PositionId,
            request.Quantity,
            request.PurchasePrice);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplySetService.CreateSupplySet(supplySet!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> UpdateSupplySet(long id, [FromBody] SupplySetUpdateRequest request, CancellationToken ct)
    {
        var model = new SupplySetUpdateModel(
            request.Quantity,
            request.PurchasePrice);

        var Id = await _supplySetService.UpdateSupplySet(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteSupplySet(long id, CancellationToken ct)
    {
        var Id = await _supplySetService.DeleteSupplySet(id, ct);

        return Ok(Id);
    }
}
