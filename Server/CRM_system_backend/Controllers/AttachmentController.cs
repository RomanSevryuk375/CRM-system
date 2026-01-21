using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Attachment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Attachment;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly IMapper _mapper;

    public AttachmentController(
        IAttachmentService attachmentService,
        IMapper mapper)
    {
        _attachmentService = attachmentService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AttachmentItem>>> GetPagedAttachments([FromQuery]AttachmentFilter filter, CancellationToken ct)
    {
        var dto = await _attachmentService.GetPagedAttachments(filter, ct);
        var count = await _attachmentService.GetCountAttachment(filter, ct);

        var response = _mapper.Map<List<AttachmentResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> CreateAttachment([FromBody]AttachmentRequest request, CancellationToken ct)
    {
        var (attachment, errors) = Attachment.Create(
            0,
            request.OrderId,
            request.WorkerId,
            request.CreateAt,
            request.Description);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _attachmentService.CreateAttachment(attachment!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> UpdateAttachment(long id, [FromBody] AttachmentUpdateRequest request, CancellationToken ct)
    {
        var Id = await _attachmentService.UpdateAttachment(id, request.Description, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> DeletingAttachment(long id, CancellationToken ct)
    {
        var Id = await _attachmentService.DeletingAttachment(id, ct);

        return Ok(Id);
    }
}
