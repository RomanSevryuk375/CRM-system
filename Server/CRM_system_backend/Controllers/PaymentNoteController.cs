using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.PaymentNote;
using Shared.Enums;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/payment-notes")]
public class PaymentNoteController(
    IPaymentNoteService paymentNoteService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<PaymentNote>>> GetPaymentNote(
        [FromQuery] PaymentNoteFilter filter, CancellationToken ct)
    {
        var dto  = await paymentNoteService.GetPagedPaymentNotes(filter, ct);
        var count = await paymentNoteService.GetCountPaymentNotes(filter, ct);

        var  response = mapper.Map<List<PaymentNoteResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreatePaymentNote(
        [FromBody] PaymentNoteRequest request, CancellationToken ct)
    {
        var (paymentNote, errors) = PaymentNote.Create(
            0,
            request.BillId,
            request.Date,
            request.Amount,
            request.MethodId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        await paymentNoteService.CreatePaymentNote(paymentNote!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdatePaymentNote(
        long id, [FromBody] PaymentMethodEnum? method, CancellationToken ct)
    {
        await paymentNoteService.UpratePaymentNote(id, method, ct);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePaymentNote(int id, CancellationToken ct)
    {
        await paymentNoteService.DeletePaymentNote(id, ct);

        return NoContent();
    }
}
