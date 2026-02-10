using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/tax-types")]
[ApiController]
public class TaxTypeController(
    ITaxTypeService taxTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<TaxTypeItem>>> GetTaxTypes(CancellationToken ct)
    {
        var dto = await taxTypeService.GetTaxTypes(ct);

        var response = mapper.Map<List<TaxTypeResponse>>(dto);

        return Ok(response);
    }
}
