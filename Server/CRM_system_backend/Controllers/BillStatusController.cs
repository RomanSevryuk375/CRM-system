using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<BillStatusItem>>> GetAllBillStatuses()
    {
        var dto = await _billStatusService.GetAllBillStatuses();

        var response = _mapper.Map<List<BillStatusResponse>>(dto);

        return Ok(response);
    }
}
