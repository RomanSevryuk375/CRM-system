using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/car-statuses")]
public class CarStatusController(
    ICarStatusService carStatusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<CarStatusResponse>>> GetCarStatuses(
        CancellationToken ct)
    {
        var dto = await carStatusService.GetCarStatuses(ct);
        var response = mapper.Map<List<CarStatusResponse>>(dto);

        return Ok(response);
    }
}
