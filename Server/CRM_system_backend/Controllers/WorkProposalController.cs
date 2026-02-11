using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
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
    public async Task<ActionResult<List<WorkProposalItem>>> GetPagedProposals(
        [FromQuery] WorkProposalFilter filter, CancellationToken ct)
    {
        var dto = await workProposalService.GetPagedProposals(filter, ct);
        var count = await workProposalService.GetCountProposals(filter, ct);

        var response = mapper.Map<List<WorkProposalResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<WorkProposalItem>> GetProposalById(
        long id, CancellationToken ct)
    {
        var dto = await workProposalService.GetProposalById(id, ct);

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> CreateProposal(
        [FromBody] WorkProposalRequest request, CancellationToken ct)
    {
        var (workProposal, errors) = WorkProposal.Create(
            0,
            request.OrderId,
            request.JobId,
            request.WorkerId,
            request.StatusId,
            request.Date);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var proposalId = await workProposalService.CreateProposal(workProposal!, ct);

        return CreatedAtAction(
            nameof(GetProposalById), 
            new { Id = proposalId }, 
            null);
    }

    [HttpPut("{id}/status")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> PathcStatusProposal(
        long id, [FromBody] ProposalStatusRequest request, CancellationToken ct)
    {
        if (request.Status == ProposalStatusEnum.Accepted)
        {
            await workProposalService.AcceptProposal(id, ct);
        }
        else if (request.Status == ProposalStatusEnum.Rejected)
        {
            await workProposalService.RejectProposal(id, ct);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteWorkProposal(long id, CancellationToken ct)
    {
        await workProposalService.DeleteProposal(id, ct);

        return NoContent();
    }
}
