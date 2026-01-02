using CRM_system_backend.Contracts.AttachmentImg;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentImgController : ControllerBase
{
    private readonly IAttachmentImgService _attachmentImgService;

    public AttachmentImgController(IAttachmentImgService attachmentImgService)
    {
        _attachmentImgService = attachmentImgService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttachmentImgItem>>> GetPagedAttachmentImg([FromQuery] AttachmentImgFilter filter)
    {
        var dto = await _attachmentImgService.GetPagedAttachmentImg(filter);
        var count = await _attachmentImgService.GetCountAttachmentImg(filter);

        var response = dto.Select(a => new AttachmentImgResponse(
            a.id,
            a.attachmentId,
            a.filePath,
            a.description));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadImage(long id)
    {
        var (stream, contentType) = await _attachmentImgService.GetImageStream(id);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAttachmentImg([FromBody] AttachmentImgRequest request)
    {
        if(request.File is null || request.File.Length == 0)
            return BadRequest("File is required");

        using var stream = request.File.OpenReadStream();
        var fileItem = new FileItem(stream, request.File.FileName, request.File.ContentType);

        var Id = await _attachmentImgService.CreateAttachmentImg(request.AttachmentId, fileItem, request.Description);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAttaachmentImg(long id, [FromBody] AttachmentImgUpdateRequest request)
    {
        var Id = await _attachmentImgService.UpdateAttaachmentImg(id, request.filePath, request.description);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAttachmentImg(long id)
    {
        var Id = await _attachmentImgService.DeleteAttachmentImg(id);

        return Ok(Id);
    }
}
