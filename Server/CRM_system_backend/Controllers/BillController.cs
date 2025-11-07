using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
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

    public async Task<ActionResult<List<Bill>>> GetBill()
    {
        var bills = await _billService.GatBill();

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

    public async Task<ActionResult<int>> CreateBill([FromBody] BillRequest request)
    {
        var (bill, error) = Bill.Create(
            0,
            request.OrderId,
            request.StatusId,
            request.Date,
            0,
            request.ActualBillDate);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var billId = await _billService.CreateBill(bill);

        return Ok(billId);
    }
}
