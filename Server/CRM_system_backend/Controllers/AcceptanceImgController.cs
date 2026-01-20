// Ignore Spelling: Img

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AccetanceImg;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AcceptanceImg;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AcceptanceImgController : ControllerBase
{
    private readonly IAcceptanceImgService _acceptanceImgService;
    private readonly IMapper _mapper;

    public AcceptanceImgController(
        IAcceptanceImgService acceptanceImgService,
        IMapper mapper)
    {
        _acceptanceImgService = acceptanceImgService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AcceptanceImgItem>>> GetAcceptanceIng([FromQuery]AcceptanceImgFilter filter, CancellationToken ct)
    {
        var dto = await _acceptanceImgService.GetAcceptanceIng(filter, ct);
        var count = await _acceptanceImgService.GetCountAcceptanceImg(filter, ct);

        var response = _mapper.Map<List<AcceptanceImgResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadImage(long id, CancellationToken ct)
    {
        var (stream, contentType) = await _acceptanceImgService.GetImageStream(id, ct);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAcceptanceImg([FromForm] CreateAcceptanceImgRequest request, CancellationToken ct)
    {
        if (request.File is null || request.File.Length == 0) 
            return BadRequest("File is required");

        using var strem = request.File.OpenReadStream();
        var fileItem = new FileItem(strem, request.File.FileName, request.File.ContentType);

        var Id = await _acceptanceImgService.CreateAcceptanceImg(request.AcceptanceId, fileItem, request.Description, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAcceptanceImg(long id, [FromBody] AcceptanceImgUpdateRequest request, CancellationToken ct)
    {
        var Id = await _acceptanceImgService.UpdateAcceptanceImg(id, request.FilePath, request.Description, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAcceptanceImg(long id, CancellationToken ct)
    {
        var Id = await _acceptanceImgService.DeleteAcceptanceImg(id, ct);

        return Ok(Id);
    }
}
