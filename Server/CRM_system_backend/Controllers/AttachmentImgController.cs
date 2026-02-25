using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
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
    public async Task<ActionResult<List<AttachmentImgResponse>>> GetPagedAttachmentImg(
        [FromQuery] AttachmentImgFilter filter, CancellationToken ct)
    {
        var dto = await attachmentImgService.GetPagedAttachmentImg(filter, ct);
        var count = await attachmentImgService.GetCountAttachmentImg(filter, ct);

        var response = mapper.Map<List<AttachmentImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<AttachmentImgResponse>> GetAttachmentImgBtId(
        int id, CancellationToken ct)
    {
        var dto = await attachmentImgService.GetAttachmentImgById(id, ct);
        var response = mapper.Map<AttachmentImgResponse>(dto);

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
        if (request.File.Length == 0)
        {
            return BadRequest("File is required");
        }

        await using var stream = request.File.OpenReadStream();
        var fileItem = new FileItem(stream, request.File.FileName, request.File.ContentType);

        var id = await attachmentImgService.CreateAttachmentImg(
            request.AttachmentId, fileItem, request.Description, ct);

        var createdDto = await attachmentImgService.GetAttachmentImgById(id, ct);
        var response = mapper.Map<AttachmentImgResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetAttachmentImgBtId),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAttachmentImg(
        long id, [FromBody] AttachmentImgUpdateRequest request, CancellationToken ct)
    {
        await attachmentImgService.UpdateAttachmentImg(id, request.FilePath, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAttachmentImg(
        long id, CancellationToken ct)
    {
        await attachmentImgService.DeleteAttachmentImg(id, ct);

        return NoContent();
    }
}
