using CRM_system_backend.Contracts.AcceptanceImg;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Services;
using CRMSystem.Buisness.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.DTOs.AccetanceImg;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AcceptanceImgController : ControllerBase
{
    private readonly IAcceptanceImgService _acceptanceImgService;

    public AcceptanceImgController(IAcceptanceImgService acceptanceImgService)
    {
        _acceptanceImgService = acceptanceImgService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AcceptanceImgItem>>> GetAcceptanceIng([FromQuery]AcceptanceImgFilter filter)
    {
        var dto = await _acceptanceImgService.GetAcceptanceIng(filter);
        var count = await _acceptanceImgService.GetCountAccptnceImg(filter);

        var response = dto.Select(a => new AcceptanceImgResponse(
            a.Id,
            a.acceptanceId,
            a.FilePath,
            a.Description));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadImage(long id)
    {
        var (stream, contentType) = await _acceptanceImgService.GetImageStream(id);

        return File(stream, contentType, $"attachment_{id}.jpg");
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAcceptanceImg([FromForm] CreateAcceptanceImgRequest request)
    {
        if (request.File is null || request.File.Length == 0) 
            return BadRequest("File is required");

        using var strem = request.File.OpenReadStream();
        var fileItem = new FileItem(strem, request.File.FileName, request.File.ContentType);

        var Id = await _acceptanceImgService.CreateAccptanceImg(request.AcceptanceId, fileItem, request.Description);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAcceptanceImg(long id, [FromBody] AcceptanceImgUpdateRequest request)
    {
        var Id = await _acceptanceImgService.UpdateAccptanceImg(id, request.FilePath, request.Description);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAcceptanceImg(long id)
    {
        var Id = await _acceptanceImgService.DeleteAccptanceImg(id);

        return Ok(Id);
    }
}
