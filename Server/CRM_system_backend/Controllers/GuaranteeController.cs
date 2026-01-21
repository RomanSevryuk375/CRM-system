using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Guarantee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Guarantee;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GuaranteeController : ControllerBase
{
    private readonly IGuaranteeService _guaranteeService;
    private readonly IMapper _mapper;

    public GuaranteeController(
        IGuaranteeService guaranteeService,
        IMapper mapper)
    {
        _guaranteeService = guaranteeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<GuaranteeItem>>> GetPagedGuarantees([FromQuery]GuaranteeFilter filter, CancellationToken ct)
    {
        var dto = await _guaranteeService.GetPagedGuarantees(filter, ct);
        var count = await _guaranteeService.GetCountGuarantees(filter, ct);

        var response = _mapper.Map<List<GuaranteeResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateGuarantee(GuaranteeRequest request, CancellationToken ct)
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

        var Id = await _guaranteeService.CreateGuarantee(guarantee!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> UpdateGuarantee(long id, GuaranteeUpdateRequest request, CancellationToken ct)
    {
        var model = new GuaranteeUpdateModel(
            request.Description,
            request.Terms);

        var Id = await _guaranteeService.UpdateGuarantee(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteGuarantee(long id, CancellationToken ct)
    {
        var Id = await _guaranteeService.DeleteGuarantee(id, ct);

        return Ok(Id);
    }
}
