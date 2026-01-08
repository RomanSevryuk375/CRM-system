using AutoMapper;
using CRM_system_backend.Contracts.Bill;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BillController : ControllerBase
{
    private readonly IBillService _billService;
    private readonly IMapper _mapper;

    public BillController(
        IBillService billService,
        IMapper mapper)
    {
        _billService = billService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillItem>>> GetPagedBills([FromQuery] BillFilter filter, CancellationToken ct)
    {
        var dto = await _billService.GetPagedBills(filter, ct);
        var count = await _billService.GetCountBills(filter, ct);

        var response = _mapper.Map<List<BillResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateBill([FromBody] BillRequest request, CancellationToken ct)
    {
        var (bill, errors) = Bill.Create(
            0,
            request.OrderId,
            request.StatusId,
            request.CreatedAt,
            request.Amount,
            request.ActualBillDate);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _billService.CreateBill(bill!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateBill(long id, [FromBody]BillUpdateRequest request, CancellationToken ct)
    {
        var model = _mapper.Map<BillUpdateModel>(request);

        var Id = await _billService.UpdateBill(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> Delete(long id, CancellationToken ct)
    {
        var Id = await _billService.Delete(id, ct);

        return Ok(Id);
    }
}
