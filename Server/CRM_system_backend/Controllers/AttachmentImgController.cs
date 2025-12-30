using CRM_system_backend.Contracts.AttachmentImg;
using CRMSystem.Buisnes.Abstractions;
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

    [HttpPost]
    public async Task<ActionResult<long>> CreateAttachmentImg([FromBody] AttachmentImgRequest request)
    {
        var (attachmentImg, errors) = AttachmentImg.Create(
            0,
            request.attachmentId,
            request.filePath,
            request.description);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _attachmentImgService.CreateAttachmentImg(attachmentImg!);

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
