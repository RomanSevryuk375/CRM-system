using AutoMapper;
using CRM_system_backend.Contracts.Tax;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<TaxItem>>> GetTaxes([FromQuery] TaxFilter filter)
    {
        var dto = await _taxService.GetTaxes(filter);

        var response = _mapper.Map<List<TaxResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTax([FromBody] TaxRequest taxRequest)
    {
        var (tax, errors) = Tax.Create(
            0,
            taxRequest.Name,
            taxRequest.Rate,
            taxRequest.TypeId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _taxService.CreateTax(tax!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateTax(int id, [FromBody] TaxUpdateRequest request)
    {
        var model = new TaxUpdateModel(
            request.Name,
            request.Rate);

        var result = await _taxService.UpdateTax(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteTax(int id)
    {
        var result = await _taxService.DeleteTax(id);

        return Ok(result);
    }
}
