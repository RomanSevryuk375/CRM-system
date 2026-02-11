using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/work-proposal-statuses")]
[ApiController]
public class WorkProposalStatusController(
    IWorkProposalStatusService service,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkProposalStatusItem>>> GetProposalStatuses(CancellationToken ct)
    {
        var dto = await service.GetProposalStatuses(ct);

        var response = mapper.Map<List<WorkProposalStatusResponse>>(dto);

        return Ok(response);
    }
}
