using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Tax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Tax;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/taxes")]
public class TaxController(
    ITaxService taxService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<TaxItem>>> GetTaxes(
        [FromQuery] TaxFilter filter, CancellationToken ct)
    {
        var dto = await taxService.GetTaxes(filter, ct);

        var response = mapper.Map<List<TaxResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateTax(
        [FromBody] TaxRequest taxRequest, CancellationToken ct)
    {
        var (tax, errors) = Tax.Create(
            0,
            taxRequest.Name,
            taxRequest.Rate,
            taxRequest.TypeId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        await taxService.CreateTax(tax!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateTax(
        int id, [FromBody] TaxUpdateRequest request, CancellationToken ct)
    {
        var model = new TaxUpdateModel(
            request.Name,
            request.Rate);

        await taxService.UpdateTax(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteTax(int id, CancellationToken ct)
    {
        await taxService.DeleteTax(id, ct);

        return NoContent();
    }
}
