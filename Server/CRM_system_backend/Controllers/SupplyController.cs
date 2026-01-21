using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Supply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Supply;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplyController : ControllerBase
{
    private readonly ISupplyService _supplyService;
    private readonly IMapper _mapper;

    public SupplyController(
        ISupplyService supplyService,
        IMapper mapper)
    {
        _supplyService = supplyService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<SupplyItem>>> GetPagedSupplies([FromQuery]SupplyFilter filter, CancellationToken ct)
    {
        var dto = await _supplyService.GetPagedSupplies(filter, ct);
        var count = await _supplyService.GetCountSupplies(filter, ct);

        var response = _mapper.Map<List<SupplyResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateSupply(SupplyRequest request, CancellationToken ct)
    {
        var (supply, errors) = Supply.Create(
            0,
            request.SupplierId,
            request.Date);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _supplyService.CreateSupply(supply!, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteSupply(long id, CancellationToken ct)
    {
        var Id = await _supplyService.DeleteSupply(id, ct);

        return Ok(Id);
    }
}
