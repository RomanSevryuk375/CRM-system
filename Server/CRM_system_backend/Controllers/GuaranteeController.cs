using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Guarantee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Guarantee;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/guarantees")]

public class GuaranteeController(
    IGuaranteeService guaranteeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<GuaranteeItem>>> GetPagedGuarantees(
        [FromQuery]GuaranteeFilter filter, CancellationToken ct)
    {
        var dto = await guaranteeService.GetPagedGuarantees(filter, ct);
        var count = await guaranteeService.GetCountGuarantees(filter, ct);

        var response = mapper.Map<List<GuaranteeResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateGuarantee(
        GuaranteeRequest request, CancellationToken ct)
    {
        var (guarantee, errors) = Guarantee.Create(
            0,
            request.OrderId,
            request.DateStart,
            request.DateEnd, 
            request.Description, 
            request.Terms);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await guaranteeService.CreateGuarantee(guarantee!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateGuarantee(
        long id, GuaranteeUpdateRequest request, CancellationToken ct)
    {
        var model = new GuaranteeUpdateModel(
            request.Description,
            request.Terms);

        await guaranteeService.UpdateGuarantee(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteGuarantee(
        long id, CancellationToken ct)
    {
        await guaranteeService.DeleteGuarantee(id, ct);

        return NoContent();
    }
}
