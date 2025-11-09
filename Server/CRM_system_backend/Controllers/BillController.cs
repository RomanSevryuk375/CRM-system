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
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<Bill>>> GetBill()
    {
        var bills = await _billService.GetBill();

        var response = bills
            .Select(b => new BillResponse(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate))
            .ToList();

        return Ok(response);
    }

    [HttpGet("My")]
    [Authorize(Policy = "UserPolicy")]

    public async Task<ActionResult<List<Bill>>> GetBillByUserId()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var bills = await _billService.GetBillByUser(userId);

        var response = bills
            .Select(b => new BillResponse(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate))
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]

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
