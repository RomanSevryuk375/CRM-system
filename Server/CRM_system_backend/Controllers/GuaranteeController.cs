using AutoMapper;
using CRMSystem.Business.Abstractions;
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
    
    public async Task<ActionResult> CreateGuarantee(
        GuaranteeRequest request, CancellationToken ct)
    {
        var createModel =mapper.Map<GuaranteeCreateModel>(request);

        await guaranteeService.CreateGuarantee(createModel, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateGuarantee(
        long id, GuaranteeUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<GuaranteeUpdateModel>(request);

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
