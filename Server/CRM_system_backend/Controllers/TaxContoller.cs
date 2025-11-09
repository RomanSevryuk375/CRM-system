using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TaxContoller : ControllerBase
{
    private readonly ITaxService _taxService;

    public TaxContoller(ITaxService taxService)
    {
        _taxService = taxService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<Tax>>> GetTaxes()
    {
        var taxes = await _taxService.GetTaxes();

        var response = taxes
            .Select(t => new TaxResponse(
                t.Id, t.Name, t.Rate, t.Type))
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> CreateTax([FromBody] TaxRequest taxRequest)
    {
        var (tax, error) = Tax.Create(
            0,
            taxRequest.Name,
            taxRequest.Rate,
            taxRequest.Type);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var taxId = await _taxService.CreateTax(tax);

        return Ok(taxId);
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdateTax([FromBody] TaxRequest taxRequest, int id)
    {
        var result = await _taxService.UpdateTax(id, taxRequest.Name, taxRequest.Rate, taxRequest.Type);

        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> DeleteTax(int id)
    {
        var result = await _taxService.DeleteTax(id);

        return Ok(result);
    }
}
