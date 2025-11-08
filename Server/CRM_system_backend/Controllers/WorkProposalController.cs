using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
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

    public async Task<ActionResult<List<WorkProposal>>> GetWorkProposal()
    {
        var workPropossals = await _workPropossalService.GetWorkProposal();

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

        return Ok(response);
    }

    [HttpGet("with-info")]

    public async Task<ActionResult<List<WorkProposalWithInfoDto>>> GetWorkProposalWithInfo()
    {
        var dtos = await _workPropossalService.GetWorkProposalWithInfo();

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

        return Ok(response);
    }

    [HttpPost]

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

    public async Task<ActionResult<int>> AcceptProposal(int id)
    {
        await _workPropossalService.AcceptProposal(id);

        return Ok(0);
    }

    [HttpPut("{id}/reject")]

    public async Task<ActionResult<int>> RejectProposal(int id)
    {
        await _workPropossalService.RejectProposal(id);

        return Ok(0);
    }


    [HttpDelete("{id}")]

    public async Task<ActionResult<int>> DeleteWorkProposa(int id)
    {
        var result = await _workPropossalService.DeleteWorkProposal(id);

        return Ok(result);
    }
}
