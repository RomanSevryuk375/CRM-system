// Ignore Spelling: Img

using AutoMapper;
using CRM_system_backend.Contracts.AttachmentImg;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentImgController : ControllerBase
{
    private readonly IAttachmentImgService _attachmentImgService;
    private readonly IMapper _mapper;

    public AttachmentImgController(
        IAttachmentImgService attachmentImgService,
        IMapper mapper)
    {
        _attachmentImgService = attachmentImgService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AttachmentImgItem>>> GetPagedAttachmentImg([FromQuery] AttachmentImgFilter filter, CancellationToken ct)
    {
        var dto = await _attachmentImgService.GetPagedAttachmentImg(filter, ct);
        var count = await _attachmentImgService.GetCountAttachmentImg(filter, ct);

        var response = _mapper.Map<List<AttachmentImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadImage(long id, CancellationToken ct)
    {
        var (stream, contentType) = await _attachmentImgService.GetImageStream(id, ct);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAttachmentImg([FromBody] AttachmentImgRequest request, CancellationToken ct)
    {
        if(request.File is null || request.File.Length == 0)
            return BadRequest("File is required");

        using var stream = request.File.OpenReadStream();
        var fileItem = new FileItem(stream, request.File.FileName, request.File.ContentType);

        var Id = await _attachmentImgService.CreateAttachmentImg(request.AttachmentId, fileItem, request.Description, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAttachmentImg(long id, [FromBody] AttachmentImgUpdateRequest request, CancellationToken ct)
    {
        var Id = await _attachmentImgService.UpdateAttachmentImg(id, request.FilePath, request.Description, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAttachmentImg(long id, CancellationToken ct)
    {
        var Id = await _attachmentImgService.DeleteAttachmentImg(id, ct);

        return Ok(Id);
    }
}
