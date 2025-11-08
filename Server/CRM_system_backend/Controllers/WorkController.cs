using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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

    public async Task<ActionResult<List<Work>>> GetWork()
    {
        var works = await _workService.GetWork();

        var response = works 
            .Select(w => new WorkResponse(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId))
            .ToList();

        return Ok(response);
    }

    [HttpGet("with-info")]

    public async Task<ActionResult<List<WorkWithInfoDto>>> GetWorkWithInfo()
    {
        var dtos = await _workService.GetWorkWithInfo();

        var response = dtos
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
    public async Task<ActionResult<int>> CreateWork([FromBody] WorkRequest request)
    {
        var (work, error) = Work.Create(
            0,
            request.OrderId,
            request.JobId,
            request.WorkerId,
            request.TimeSpent,
            request.StatusId);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var workId = await _workService.CreateWork(work);

        return Ok(workId);
    }

    [HttpPut]

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

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteWork(int id)
    {
        var result = await _workService.DeleteWork(id);

        return Ok(result);
    }
}
