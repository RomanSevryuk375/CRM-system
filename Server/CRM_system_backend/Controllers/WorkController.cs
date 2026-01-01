using AutoMapper.Configuration.Annotations;
using CRM_system_backend.Contracts.Work;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Work;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkItem>>> GetPagedWork([FromQuery] WorkFilter filter)
    {
        var dto = await _workService.GetPagedWork(filter);
        var count = await _workService.GetCountWork();

        var response = dto.Select(w => new WorkResponse(
                w.id,
                w.title,
                w.categoty,
                w.description,
                w.standartTime));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateWork([FromBody] WorkRequest request)
    {
        var (work, errors) = Work.Create(
            0,
            request.title,
            request.categoty,
            request.description,
            request.standartTime);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var workId = await _workService.CreateWork(work!);

        return Ok(workId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateWork(long id, [FromBody] WorkRequest request)
    {
        var model = new WorkUpdateModel(
            request.title,
            request.categoty,
            request.description,
            request.standartTime);

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
