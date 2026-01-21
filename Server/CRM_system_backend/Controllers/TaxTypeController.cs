using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaxTypeController : ControllerBase
{
    private readonly ITaxTypeService _taxTypeService;
    private readonly IMapper _mapper;

    public TaxTypeController(
        ITaxTypeService taxTypeService,
        IMapper mapper)
    {
        _taxTypeService = taxTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<TaxTypeItem>>> GetTaxTypes(CancellationToken ct)
    {
        var dto = await _taxTypeService.GetTaxTypes(ct);

        var response = _mapper.Map<List<TaxTypeResponse>>(dto);

        return Ok(response);
    }
}
