using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/tax-types")]
public class TaxTypeController(
    ITaxTypeService taxTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<TaxTypeResponse>>> GetTaxTypes(CancellationToken ct)
    {
        var dto = await taxTypeService.GetTaxTypes(ct);
        var response = mapper.Map<List<TaxTypeResponse>>(dto);

        return Ok(response);
    }
}
