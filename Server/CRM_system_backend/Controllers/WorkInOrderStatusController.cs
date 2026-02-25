using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/work-in-order-statuses")]
public class WorkInOrderStatusController(
    IWorkInOrderStatusService service,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkInOrderStatusResponse>>> GetWiOStatuses(CancellationToken ct)
    {
        var dto = await service.GetWiOStatuses(ct);
        var response = mapper.Map<List<WorkInOrderStatusResponse>>(dto);

        return Ok(response);
    }
}
