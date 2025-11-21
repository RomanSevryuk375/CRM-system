using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;

    public BillController(IBillService billService)
    {
        _billService = billService;
    }

    [HttpGet]
    //[Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<Bill>>> GetBill(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {

        var bills = await _billService.GetPagedBill(page, limit);
        var totalCount = await _billService.GetBillCount();

        var response = bills
            .Select(b => new BillResponse(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("My")]
    //[Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<List<Bill>>> GetBillByUserId(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var bills = await _billService.GetPagedBillByUser(userId, page, limit);
        var totalCount = await _billService.GetBillCountByUser(userId);

        var response = bills
            .Select(b => new BillResponse(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpPost]
    //[Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> CreateBill([FromBody] BillRequest request)
    {
        var (bill, error) = Bill.Create(
            0,
            request.OrderId,
            request.StatusId,
            request.Date,
            0,
            null);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var billId = await _billService.CreateBill(bill);

        return Ok(billId);
    }
}
