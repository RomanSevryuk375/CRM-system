using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<CarStatusItem>>> GetCarStatuses()
    {
        var dto = await _carStatusService.GetCarStatuses();

        var response = _mapper.Map<List<CarStatusResponse>>(dto);

        return Ok(response);
    }
}
