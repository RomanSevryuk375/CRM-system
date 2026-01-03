using CRM_system_backend.Contracts.Guarantee;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Guarantee;
using CRMSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GuaranteeController : ControllerBase
{
    private readonly IGuaranteeService _guaranteeService;

    public GuaranteeController(IGuaranteeService guaranteeService)
    {
        _guaranteeService = guaranteeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GuaranteeItem>>> GetPagedGuarantees([FromQuery]GuaranteeFilter filter)
    {
        var dto = await _guaranteeService.GetPagedGuarantees(filter);
        var count = await _guaranteeService.GetCountGuarantees(filter);

        var response = dto.Select(g => new GuaranteeResponse(
            g.Id,
            g.OrderId,
            g.DateStart,
            g.DateEnd,
            g.Description,
            g.Terms));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateGuarantee(GuaranteeRequest request)
    {
        var (guarantee, errors) = Guarantee.Create(
            0,
            request.OrderId,
            request.DateStart,
            request.DateEnd, 
            request.Description, 
            request.Terms);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _guaranteeService.CreateGuarantee(guarantee!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateGuarantee(long id, GuaranteeUpdateRequest request)
    {
        var model = new GuaranteeUpdateModel(
            request.Description,
            request.Terms);

        var Id = await _guaranteeService.UpdateGuarantee(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteGuarantee(long id)
    {
        var Id = await _guaranteeService.DeleteGuarantee(id);

        return Ok(Id);
    }
}
