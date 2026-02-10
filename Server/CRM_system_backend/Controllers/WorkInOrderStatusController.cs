using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/work-in-order-statuses")]
[ApiController]
public class WorkInOrderStatusController(
    IWorkInOrderStatusService service,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkInOrderStatusItem>>> GetWiOStatuses(CancellationToken ct)
    {
        var dto = await service.GetWiOStatuses(ct);

        var response = mapper.Map<List<WorkInOrderStatusResponse>>(dto);

        return Ok(response);
    }
}
