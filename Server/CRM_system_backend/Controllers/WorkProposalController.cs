using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkProposalController : ControllerBase
{
    private readonly IWorkPropossalService _workPropossalService;

    public WorkProposalController(IWorkPropossalService workPropossalService)
    {
        _workPropossalService = workPropossalService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkProposal>>> GetWorkProposal(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var workPropossals = await _workPropossalService.GetPagedWorkProposal(page, limit);
        var totalCount = await _workPropossalService.GetCountProposal();

        var response = workPropossals
            .Select(wp => new WorkProposalResponse(
                wp.Id,
                wp.OrderId,
                wp.WorkId,
                wp.ByWorker,
                wp.StatusId,
                wp.DecisionStatusId,
                wp.Date))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkProposalWithInfoDto>>> GetWorkProposalWithInfo(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _workPropossalService.GetPagedWorkProposalWithInfo(page, limit);
        var totalCount = await _workPropossalService.GetCountProposal();

        var response = dtos
            .Select(d => new WorkProposalWithInfoDto(
                d.Id,
                d.OrderId,
                d.WorkName,
                d.ByWorker,
                d.StatusName,
                d.DecisionStatusName,
                d.Date))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("InWork")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<List<WorkerWithInfoDto>>> GetProposalForClient(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var proposals = await _workPropossalService.GetPagedProposalForClient(userId, page, limit);
        var totalCount = await _workPropossalService.GetCountProposalForClient(userId);

        var response = proposals
            .Select(d => new WorkProposalWithInfoDto(
                d.Id,
                d.OrderId,
                d.WorkName,
                d.ByWorker,
                d.StatusName,
                d.DecisionStatusName,
                d.Date))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<int>> CreateWorkProposal([FromBody] WorkProposalRequest request)
    {
        var (workProposal, error) = WorkProposal.Create(
            0,
            request.OrderId,
            request.WorkId,
            request.ByWorker,
            request.StatusId,
            request.DecisionStatusId,
            request.Date);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var workProposalId = await _workPropossalService.CreateWorkProposal(workProposal);

        return Ok(workProposalId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateWorkProposa([FromBody]WorkProposalRequest request, int id)
    {
        var result = await _workPropossalService.UpdateWorkProposal(id, 
            request.OrderId,
            request.WorkId,
            request.ByWorker,
            request.StatusId,
            request.DecisionStatusId,
            request.Date);

        return Ok(result);
    }

    [HttpPut("{id}/accept")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<int>> AcceptProposal(int id)
    {
        await _workPropossalService.AcceptProposal(id);

        return Ok(0);
    }

    [HttpPut("{id}/reject")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<int>> RejectProposal(int id)
    {
        await _workPropossalService.RejectProposal(id);

        return Ok(0);
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<int>> DeleteWorkProposa(int id)
    {
        var result = await _workPropossalService.DeleteWorkProposal(id);

        return Ok(result);
    }
}
