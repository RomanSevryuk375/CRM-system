using CRM_system_backend.Contracts.AcceptanceImg;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.AcceptanceImg;
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
            a.id,
            a.acceptanceId,
            a.filePath,
            a.description));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateAcceptanceImg([FromBody] AcceptanceImgRequest request)
    {
        var (acceptanceImg, errors) = AcceptanceImg.Create(
            0,
            request.acceptanceId,
            request.filePath,
            request.description);

        if (errors is not null && errors.Any()) 
            return BadRequest(errors);

        var Id = await _acceptanceImgService.CreateAccptanceImg(acceptanceImg!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateAcceptanceImg(long id, [FromBody] AcceptanceImgUpdateRequest request)
    {
        var Id = await _acceptanceImgService.UpdateAccptanceImg(id, request.filePath, request.description);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteAcceptanceImg(long id)
    {
        var Id = await _acceptanceImgService.DeleteAccptanceImg(id);

        return Ok(Id);
    }
}
