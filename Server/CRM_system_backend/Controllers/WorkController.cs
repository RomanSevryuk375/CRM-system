using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<Work>>> GetPagedWork(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var works = await _workService.GetPagedWork(page, limit);
        var totalCount = await _workService.GetCountWork();

        var response = works 
            .Select(w => new WorkResponse(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("MyWorks")] //not tested
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<List<WorkWithInfoDto>>> GetWorkByWorkerId(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        //workerId
        var works = await _workService.GetPagedInWorkWorks(userId, page, limit);
        var totalCount = await _workService.GetCoutInWorkWorks(userId);

        var response = works
            .Select(w => new WorkWithInfoDto(
                w.Id,
                w.OrderId,
                w.JobName,
                w.WorkerInfo,
                w.TimeSpent,
                w.StatusName))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("with-info")]
    public async Task<ActionResult<List<WorkWithInfoDto>>> GetWorkWithInfo(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _workService.GetPagedWorkWithInfo(page, limit);
        var totalCount = await _workService.GetCountWork();

        var response = dtos
            .Select(w => new WorkWithInfoDto(
                w.Id,
                w.OrderId,
                w.JobName,
                w.WorkerInfo,
                w.TimeSpent,
                w.StatusName))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("forCar/{carId}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkWithInfoDto>>> GetWorksForCar(int carId)
    {
        var works = await _workService.GetWorkByCarId(carId);

        var response = works
            .Select(w => new WorkWithInfoDto(
                w.Id,
                w.OrderId,
                w.JobName,
                w.WorkerInfo,
                w.TimeSpent,
                w.StatusName))
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateWork([FromBody] WorkRequest request)
    {
        var (work, error) = Work.Create(
            0,
            request.OrderId ?? 0,
            request.JobId ?? 0,
            request.WorkerId ?? 0,
            request.TimeSpent ?? 0m,
            request.StatusId ?? 0);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var workId = await _workService.CreateWork(work);

        return Ok(workId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<int>> UpdateWork([FromBody] WorkRequest request, int id)
    {
        var result = await _workService.UpdateWork(
            id,
            request.OrderId,
            request.JobId,
            request.WorkerId,
            request.TimeSpent,
            request.StatusId);

        return Ok(result);
    }

    [HttpDelete("${id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteWork(int id)
    {
        var result = await _workService.DeleteWork(id);

        return Ok(result);
    }
}
