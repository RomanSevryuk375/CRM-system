using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CarStatusController : ControllerBase
{
    private readonly ICarStatusService _carStatusService;
    private readonly IMapper _mapper;

    public CarStatusController(
        ICarStatusService carStatusService,
        IMapper mapper)
    {
        _carStatusService = carStatusService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<CarStatusItem>>> GetCarStatuses(CancellationToken ct)
    {
        var dto = await _carStatusService.GetCarStatuses(ct);

        var response = _mapper.Map<List<CarStatusResponse>>(dto);

        return Ok(response);
    }
}
