using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CarStatusController : ControllerBase
{
    private readonly ICarStatusService _carStatusService;

    public CarStatusController(ICarStatusService carStatusService)
    {
        _carStatusService = carStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarStatusItem>>> GetCarStatuses()
    {
        var dto = await _carStatusService.GetCarStatuses();

        var response = dto.Select(c => new CarStatusResponse(
            c.Id,
            c.Name));

        return Ok(response);
    }
}
