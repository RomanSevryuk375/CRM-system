using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AcceptanceImg;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/acceptance-images")]
public class AcceptanceImgController(
    IAcceptanceImgService acceptanceImgService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AcceptanceImgResponse>>> GetAcceptanceIng(
        [FromQuery]AcceptanceImgFilter filter, CancellationToken ct)
    {
        var dto = await acceptanceImgService.GetAcceptanceIng(filter, ct);
        var count = await acceptanceImgService.GetCountAcceptanceImg(filter, ct);

        var response = mapper.Map<List<AcceptanceImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<AcceptanceImgResponse>> GetAcceptanceIngById(
        int id, CancellationToken ct)
    {
        var dto = await acceptanceImgService.GetAcceptanceImgById(id, ct);
        var response = mapper.Map<AcceptanceImgResponse>(dto);
        

        return Ok(response);
    }

    [HttpGet("{id:long}/img")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<IActionResult> DownloadImage(
        long id, CancellationToken ct)
    {
        var (stream, contentType) = await acceptanceImgService.GetImageStream(id, ct);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAcceptanceImg(
        [FromForm] CreateAcceptanceImgRequest request, CancellationToken ct)
    {
        if (request.File.Length == 0)
        {
            return BadRequest("File is required");
        }

        await using var stream = request.File.OpenReadStream();
        var fileItem = new FileItem(stream, request.File.FileName, request.File.ContentType);
        var id = await acceptanceImgService.CreateAcceptanceImg(
            request.AcceptanceId, fileItem, request.Description, ct);

        var createdDto = await acceptanceImgService.GetAcceptanceImgById(id, ct);
        var response = mapper.Map<AcceptanceImgResponse>(createdDto);
        
        return CreatedAtAction(
            nameof(GetAcceptanceIngById),
            new { Id = id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAcceptanceImg(
        int id, [FromBody] AcceptanceImgUpdateRequest request, CancellationToken ct)
    {
        await acceptanceImgService.UpdateAcceptanceImg(id, request.FilePath, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAcceptanceImg(
        int id, CancellationToken ct)
    {
        await acceptanceImgService.DeleteAcceptanceImg(id, ct);

        return NoContent();
    }
}
