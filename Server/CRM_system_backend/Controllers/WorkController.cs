using AutoMapper;
using CRM_system_backend.Contracts.Work;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;
    private readonly IMapper _mapper;

    public WorkController(
        IWorkService workService,
        IMapper mapper)
    {
        _workService = workService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkItem>>> GetPagedWork([FromQuery] WorkFilter filter)
    {
        var dto = await _workService.GetPagedWork(filter);
        var count = await _workService.GetCountWork();

        var response = _mapper.Map<List<WorkResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateWork([FromBody] WorkRequest request)
    {
        var (work, errors) = Work.Create(
            0,
            request.Title,
            request.Category,
            request.Description,
            request.StandartTime);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var workId = await _workService.CreateWork(work!);

        return Ok(workId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateWork(long id, [FromBody] WorkRequest request)
    {
        var model = new WorkUpdateModel(
            request.Title,
            request.Category,
            request.Description,
            request.StandartTime);

        var Id = await _workService.UpdateWork(id, model);

        return Ok(Id);
    }

    [HttpDelete("${id}")]
    public async Task<ActionResult<long>> DeleteWork(long id)
    {
        var Id = await _workService.DeleteWork(id);

        return Ok(Id);
    }
}
