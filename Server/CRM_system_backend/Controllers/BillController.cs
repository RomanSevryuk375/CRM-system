using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Bill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Bill;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/bills")]
public class BillController(
    IBillService billService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<BillResponse>>> GetPagedBills(
        [FromQuery] BillFilter filter, CancellationToken ct)
    {
        var dto = await billService.GetPagedBills(filter, ct);
        var count = await billService.GetCountBills(filter, ct);

        var response = mapper.Map<List<BillResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<BillResponse>> GetBillById(
        int id, CancellationToken ct)
    {
        var dto = await billService.GetBillById(id, ct);
        var response = mapper.Map<BillResponse>(dto);

        return Ok(response);
    }

    [HttpGet("{id:long}/debt")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<long>> FetchDebt(
        long id, CancellationToken ct)
    {
        var debt = await billService.FetchDebtOfBill(id, ct);

        return Ok(debt);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateBill(
        [FromBody] BillRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<BillCreateModel>(request);
        var id = await billService.CreateBill(createModel, ct);
        
        var createdDto = await billService.GetBillById(id, ct);
        var response = mapper.Map<BillResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetBillById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateBill(
        long id, [FromBody]BillUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<BillUpdateModel>(request);
        await billService.UpdateBill(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Delete(long id, CancellationToken ct)
    {
        await billService.Delete(id, ct);

        return NoContent();
    }
}
