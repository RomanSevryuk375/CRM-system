// Ignore Spelling: Img

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AttachmentImg;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/attachments-images")]
public class AttachmentImgController(
    IAttachmentImgService attachmentImgService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AttachmentImgItem>>> GetPagedAttachmentImg(
        [FromQuery] AttachmentImgFilter filter, CancellationToken ct)
    {
        var dto = await attachmentImgService.GetPagedAttachmentImg(filter, ct);
        var count = await attachmentImgService.GetCountAttachmentImg(filter, ct);

        var response = mapper.Map<List<AttachmentImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<IActionResult> DownloadImage(
        long id, CancellationToken ct)
    {
        var (stream, contentType) = await attachmentImgService.GetImageStream(id, ct);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAttachmentImg(
        [FromBody] AttachmentImgRequest request, CancellationToken ct)
    {
        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("File is required");
        }

        using var stream = request.File.OpenReadStream();
        var fileItem = new FileItem(stream, request.File.FileName, request.File.ContentType);

        var Id = await attachmentImgService.CreateAttachmentImg(
            request.AttachmentId, fileItem, request.Description, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAttachmentImg(
        long id, [FromBody] AttachmentImgUpdateRequest request, CancellationToken ct)
    {
        await attachmentImgService.UpdateAttachmentImg(id, request.FilePath, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAttachmentImg(
        long id, CancellationToken ct)
    {
        await attachmentImgService.DeleteAttachmentImg(id, ct);

        return NoContent();
    }
}
