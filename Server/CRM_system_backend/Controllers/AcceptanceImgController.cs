// Ignore Spelling: Img

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AccetanceImg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AcceptanceImg;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/acceptance-images")]
[ApiController]
public class AcceptanceImgController(
    IAcceptanceImgService acceptanceImgService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AcceptanceImgItem>>> GetAcceptanceIng(
        [FromQuery]AcceptanceImgFilter filter, CancellationToken ct)
    {
        var dto = await acceptanceImgService.GetAcceptanceIng(filter, ct);
        var count = await acceptanceImgService.GetCountAcceptanceImg(filter, ct);

        var response = mapper.Map<List<AcceptanceImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
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
        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("File is required");
        }

        using var strem = request.File.OpenReadStream();
        var fileItem = new FileItem(strem, request.File.FileName, request.File.ContentType);

        await acceptanceImgService.CreateAcceptanceImg(request.AcceptanceId, fileItem, request.Description, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAcceptanceImg(
        long id, [FromBody] AcceptanceImgUpdateRequest request, CancellationToken ct)
    {
        await acceptanceImgService.UpdateAcceptanceImg(id, request.FilePath, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAcceptanceImg(
        long id, CancellationToken ct)
    {
        await acceptanceImgService.DeleteAcceptanceImg(id, ct);

        return NoContent();
    }
}
