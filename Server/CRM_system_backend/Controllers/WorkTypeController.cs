using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkTypeController : ControllerBase
{
    private readonly IWorkTypepService _workTypepService;

    public WorkTypeController(IWorkTypepService workTypepService)
    {
        _workTypepService = workTypepService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "WorkerPolicy")]

    public async Task<ActionResult<List<WorkType>>> GetWorkType()
    {
        var workTypes = await _workTypepService.GetWorkType();

        var response = workTypes
            .Select(wt => new WorkTypeResponse(
                wt.Id,
                wt.Title,
                wt.Description,
                wt.Category,
                wt.StandardTime)).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> CreateWorkType([FromBody] WorkTypeRequest workTypeRequest)
    {
        var (workType, error) = WorkType.Create(
            0,
            workTypeRequest.Title,
            workTypeRequest.Category,
            workTypeRequest.Description,
            workTypeRequest.StandardTime);

        if (!string.IsNullOrEmpty(error))
            return BadRequest(error);

        var workTypeId = await _workTypepService.CreateWorkType(workType);

        return Ok(workTypeId);
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdateWorkType([FromBody] WorkTypeRequest workTypeRequest, int id)
    {
        var result = await _workTypepService.UpdateWorkType(
            id,
            workTypeRequest.Title,
            workTypeRequest.Category,
            workTypeRequest.Description,
            workTypeRequest.StandardTime);

        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> DeleteWorkType (int id)
    {
        var result = await _workTypepService.DeleteWorkType(id);

        return Ok(result);
    }
}
