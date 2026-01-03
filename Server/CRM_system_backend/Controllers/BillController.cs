using CRM_system_backend.Contracts.Bill;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Bill;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BillController : ControllerBase
{
    private readonly IBillService _billService;

    public BillController(IBillService billService)
    {
        _billService = billService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillItem>>> GetPagedBills([FromQuery] BillFilter filter)
    {
        var dto = await _billService.GetPagedBills(filter);
        var count = await _billService.GetCountBills(filter);

        var response = dto.Select(b => new BillResponse(
            b.Id,
            b.OrderId,
            b.Status,
            b.StatusId,
            b.CreatedAt,
            b.Amount,
            b.ActualBillDate));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateBill([FromBody] BillRequest request)
    {
        var (bill, errors) = Bill.Create(
            0,
            request.OrderId,
            request.StatusId,
            request.CreatedAt,
            request.Amount,
            request.ActualBillDate);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _billService.CreateBill(bill!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateBill(long id, [FromBody]BillUpdateRequest request)
    {
        var model = new BillUpdateModel(
            request.StatusId,
            request.Amount,
            request.ActualBillDate);

        var Id = await _billService.UpdateBill(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> Delete(long id)
    {
        var Id = await _billService.Delete(id);

        return Ok(Id);
    }
}
