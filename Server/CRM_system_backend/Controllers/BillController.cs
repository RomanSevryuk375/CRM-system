using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
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
    public async Task<ActionResult<List<BillItem>>> GetPagedBills(
        [FromQuery] BillFilter filter, CancellationToken ct)
    {
        var dto = await billService.GetPagedBills(filter, ct);
        var count = await billService.GetCountBills(filter, ct);

        var response = mapper.Map<List<BillResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("debt/{id}")]
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
        var (bill, errors) = Bill.Create(
            0,
            request.OrderId,
            request.StatusId,
            request.CreatedAt,
            request.Amount,
            request.ActualBillDate);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await billService.CreateBill(bill!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateBill(
        long id, [FromBody]BillUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<BillUpdateModel>(request);

        await billService.UpdateBill(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> Delete(long id, CancellationToken ct)
    {
        await billService.Delete(id, ct);

        return NoContent();
    }
}
