using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaxTypeController : ControllerBase
{
    private readonly ITaxTypeService _taxTypeService;

    public TaxTypeController(ITaxTypeService taxTypeService)
    {
        _taxTypeService = taxTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaxTypeItem>>> GetTaxTypes()
    {
        var dto = await _taxTypeService.GetTaxTypes();

        var response = dto.Select(t => new TaxTypeResponse(
            t.Id,
            t.Name));

        return Ok(response);
    }
}
