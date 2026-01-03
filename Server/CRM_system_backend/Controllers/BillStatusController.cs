using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BillStatusController : ControllerBase
{
    private readonly IBillStatusService _billStatusService;

    public BillStatusController(IBillStatusService billStatusService)
    {
        _billStatusService = billStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillStatusItem>>> GetAllBillStatuses()
    {
        var dto = await _billStatusService.GetAllBillStatuses();

        var response = dto.Select(b => new BillStatusResponse(
            b.Id,
            b.Name));

        return Ok(response);
    }
}
