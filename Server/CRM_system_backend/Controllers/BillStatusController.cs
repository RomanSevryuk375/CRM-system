using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/bill-statuses")]

public class BillStatusController(
    IBillStatusService billStatusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<BillStatusItem>>> GetAllBillStatuses(CancellationToken ct)
    {
        var dto = await billStatusService.GetAllBillStatuses(ct);

        var response = mapper.Map<List<BillStatusResponse>>(dto);

        return Ok(response);
    }
}
