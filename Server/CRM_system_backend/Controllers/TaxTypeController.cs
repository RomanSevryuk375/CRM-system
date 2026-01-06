using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<TaxTypeItem>>> GetTaxTypes()
    {
        var dto = await _taxTypeService.GetTaxTypes();

        var response = _mapper.Map<List<TaxTypeResponse>>(dto);

        return Ok(response);
    }
}
