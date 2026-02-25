using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.WorkProposal;
using Shared.Enums;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/work-proposals")]
public class WorkProposalController(
    IWorkProposalService workProposalService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkProposalResponse>>> GetPagedProposals(
        [FromQuery] WorkProposalFilter filter, CancellationToken ct)
    {
        var dto = await workProposalService.GetPagedProposals(filter, ct);
        var count = await workProposalService.GetCountProposals(filter, ct);

        var response = mapper.Map<List<WorkProposalResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<WorkProposalResponse>> GetProposalById(
        long id, CancellationToken ct)
    {
        var dto = await workProposalService.GetProposalById(id, ct);
        var response = mapper.Map<WorkProposalResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateProposal(
        [FromBody] WorkProposalRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<WorkProposalCreateModel>(request);
        var id = await workProposalService.CreateProposal(createModel, ct);
        
        var createdDto = await workProposalService.GetProposalById(id, ct);
        var response = mapper.Map<WorkProposalResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetProposalById), 
            new { id = createdDto.Id }, 
            response);
    }

    [HttpPatch("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> PatchStatusProposal(
        long id, [FromBody] ProposalStatusRequest request, CancellationToken ct)
    {
        switch (request.Status)
        {
            case ProposalStatusEnum.Accepted:
                await workProposalService.AcceptProposal(id, ct);
                break;
            case ProposalStatusEnum.Rejected:
                await workProposalService.RejectProposal(id, ct);
                break;
        }

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteWorkProposal(long id, CancellationToken ct)
    {
        await workProposalService.DeleteProposal(id, ct);

        return NoContent();
    }
}
