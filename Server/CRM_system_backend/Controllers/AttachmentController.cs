using AutoMapper;
using CRMSystem.Business.Abstractions;
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
    public async Task<ActionResult<List<AttachmentResponse>>> GetPagedAttachments(
        [FromQuery]AttachmentFilter filter, CancellationToken ct)
    {
        var dto = await attachmentService.GetPagedAttachments(filter, ct);
        var count = await attachmentService.GetCountAttachment(filter, ct);

        var response = mapper.Map<List<AttachmentResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<AttachmentResponse>> GetAttachmentById(
        long id, CancellationToken ct)
    {
        var dto = await attachmentService.GetAttachmentById(id, ct);

        var response = mapper.Map<AttachmentResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAttachment(
        [FromBody]AttachmentRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<AttachmentCreateModel>(request);
        var id = await attachmentService.CreateAttachment(createModel, ct);

        var createdDto = await attachmentService.GetAttachmentById(id, ct);
        var response = mapper.Map<AttachmentResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetAttachmentById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAttachment(
        long id, [FromBody] AttachmentUpdateRequest request, CancellationToken ct)
    {
        await attachmentService.UpdateAttachment(id, request.Description, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeletingAttachment(
        long id, CancellationToken ct)
    {
        await attachmentService.DeletingAttachment(id, ct);

        return NoContent();
    }
}
