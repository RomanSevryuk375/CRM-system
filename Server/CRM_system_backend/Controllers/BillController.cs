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
}
