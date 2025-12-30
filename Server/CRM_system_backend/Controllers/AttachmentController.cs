using CRM_system_backend.Contracts.Attachment;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;

    public AttachmentController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttachmentItem>>> GetPagedAttachments([FromQuery]AttachmentFilter filter)
    {
        var dto = await _attachmentService.GetPagedAttachments(filter);
        var count = await _attachmentService.GetCountAttchment(filter);

        var response = dto.Select(a => new AttachmentResponse(
            a.id,
            a.orderId,
            a.worker,
            a.workerId,
            a.createAt,
            a.description));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAttachment([FromBody]AttachmentRequest request)
    {
        var (attachment, errors) = Attachment.Create(
            0,
            request.orderId,
            request.workerId,
            request.createAt,
            request.description);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _attachmentService.CreateAttachment(attachment!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAttachment(long id, [FromBody] AttachmentUpdateRequest request)
    {
        var Id = await _attachmentService.UpdateAttachment(id, request.description);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteingAttachment(long id)
    {
        var Id = await _attachmentService.DeleteingAttachment(id);

        return Ok(Id);
    }
}
