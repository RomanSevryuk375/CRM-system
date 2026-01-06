using AutoMapper;
using CRM_system_backend.Contracts.WorkPropossal;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkProposalController : ControllerBase
{
    private readonly IWorkProposalService _workPropossalService;
    private readonly IMapper _mapper;

    public WorkProposalController(
        IWorkProposalService workProposalService,
        IMapper mapper)
    {
        _workPropossalService = workProposalService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkProposalItem>>> GetPagedProposals([FromQuery] WorkProposalFilter filter)
    {
        var dto = await _workPropossalService.GetPagedProposals(filter);
        var count = await _workPropossalService.GetCountProposals(filter);

        var response = _mapper.Map<List<WorkProposalResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkProposalItem>> GetProposalById(long id)
    {
        var dto = await _workPropossalService.GetProposalById(id);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateProposal([FromBody] WorkProposalRequest request)
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

        var Id = await _workPropossalService.CreateProposal(workProposal!);

        return Ok(Id);
    }

    [HttpPut("{id}/accept")]
    public async Task<ActionResult<long>> AcceptProposal(long id)
    {
        await _workPropossalService.AcceptProposal(id);

        return Ok(0);
    }

    [HttpPut("{id}/reject")]
    public async Task<ActionResult<long>> RejectProposal(long id)
    {
        await _workPropossalService.RejectProposal(id);

        return Ok(0);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteWorkProposal(long id)
    {
        var result = await _workPropossalService.DeleteProposal(id);

        return Ok(result);
    }
}
