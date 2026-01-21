using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.PaymentNote;
using Shared.Enums;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentNoteController : ControllerBase
{
    private readonly IPaymentNoteService _paymentNoteService;
    private readonly IMapper _mapper;

    public PaymentNoteController(
        IPaymentNoteService paymentNoteService,
        IMapper mapper)
    {
        _paymentNoteService = paymentNoteService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<PaymentNote>>> GetPaymentNote([FromQuery] PaymentNoteFilter filter, CancellationToken ct)
    {
        var dto  = await _paymentNoteService.GetPagedPaymentNotes(filter, ct);
        var count = await _paymentNoteService.GetCountPaymentNotes(filter, ct);

        var  response = _mapper.Map<List<PaymentNoteResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<long>> CreatePaymentNote([FromBody] PaymentNoteRequest request, CancellationToken ct)
    {
        var (paymentNote, errors) = PaymentNote.Create(
            0,
            request.BillId,
            request.Date,
            request.Amount,
            request.MethodId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _paymentNoteService.CreatePaymentNote(paymentNote!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdatePaymentNote(long id, [FromBody] PaymentMethodEnum? method, CancellationToken ct)
    {
        var Id = await _paymentNoteService.UpratePaymentNote(id, method, ct);
        
        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeletePaymentNote(int id, CancellationToken ct)
    {
        var result = await _paymentNoteService.DeletePaymentNote(id, ct);

        return Ok(result);
    }
}
