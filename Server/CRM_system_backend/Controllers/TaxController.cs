using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Tax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Tax;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxController : ControllerBase
{
    private readonly ITaxService _taxService;
    private readonly IMapper _mapper;

    public TaxController(
        ITaxService taxService,
        IMapper mapper)
    {
        _taxService = taxService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<TaxItem>>> GetTaxes([FromQuery] TaxFilter filter, CancellationToken ct)
    {
        var dto = await _taxService.GetTaxes(filter, ct);

        var response = _mapper.Map<List<TaxResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateTax([FromBody] TaxRequest taxRequest, CancellationToken ct)
    {
        var (tax, errors) = Tax.Create(
            0,
            taxRequest.Name,
            taxRequest.Rate,
            taxRequest.TypeId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _taxService.CreateTax(tax!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateTax(int id, [FromBody] TaxUpdateRequest request, CancellationToken ct)
    {
        var model = new TaxUpdateModel(
            request.Name,
            request.Rate);

        var result = await _taxService.UpdateTax(id, model, ct);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteTax(int id, CancellationToken ct)
    {
        var result = await _taxService.DeleteTax(id, ct);

        return Ok(result);
    }
}
