using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Attachment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Attachment;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/attachments")]

public class AttachmentController(
    IAttachmentService attachmentService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AttachmentItem>>> GetPagedAttachments(
        [FromQuery]AttachmentFilter filter, CancellationToken ct)
    {
        var dto = await attachmentService.GetPagedAttachments(filter, ct);
        var count = await attachmentService.GetCountAttachment(filter, ct);

        var response = mapper.Map<List<AttachmentResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAttachment(
        [FromBody]AttachmentRequest request, CancellationToken ct)
    {
        var (attachment, errors) = Attachment.Create(
            0,
            request.OrderId,
            request.WorkerId,
            request.CreateAt,
            request.Description);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        await attachmentService.CreateAttachment(attachment!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAttachment(
        long id, [FromBody] AttachmentUpdateRequest request, CancellationToken ct)
    {
        await attachmentService.UpdateAttachment(id, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeletingAttachment(
        long id, CancellationToken ct)
    {
        await attachmentService.DeletingAttachment(id, ct);

        return NoContent();
    }
}
