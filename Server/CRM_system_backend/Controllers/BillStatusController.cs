using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BillStatusController : ControllerBase
{
    private readonly IBillStatusService _billStatusService;
    private readonly IMapper _mapper;

    public BillStatusController(
        IBillStatusService billStatusService,
        IMapper mapper)
    {
        _billStatusService = billStatusService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillStatusItem>>> GetAllBillStatuses(CancellationToken ct)
    {
        var dto = await _billStatusService.GetAllBillStatuses(ct);

        var response = _mapper.Map<List<BillStatusResponse>>(dto);

        return Ok(response);
    }
}
