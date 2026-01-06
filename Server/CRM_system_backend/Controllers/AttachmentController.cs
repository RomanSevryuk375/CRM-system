using AutoMapper;
using CRM_system_backend.Contracts.Attachment;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<AttachmentItem>>> GetPagedAttachments([FromQuery]AttachmentFilter filter)
    {
        var dto = await _attachmentService.GetPagedAttachments(filter);
        var count = await _attachmentService.GetCountAttachment(filter);

        var response = _mapper.Map<List<AttachmentResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAttachment([FromBody]AttachmentRequest request)
    {
        var (attachment, errors) = Attachment.Create(
            0,
            request.OrderId,
            request.WorkerId,
            request.CreateAt,
            request.Description);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _attachmentService.CreateAttachment(attachment!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAttachment(long id, [FromBody] AttachmentUpdateRequest request)
    {
        var Id = await _attachmentService.UpdateAttachment(id, request.Description);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeletingAttachment(long id)
    {
        var Id = await _attachmentService.DeletingAttachment(id);

        return Ok(Id);
    }
}
