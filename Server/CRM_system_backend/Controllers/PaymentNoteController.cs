using CRM_system_backend.Contracts;
using CRM_system_backend.Contracts.PaymentNote;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentNoteController : ControllerBase
{
    private readonly IPaymentNoteService _paymentNoteService;

    public PaymentNoteController(IPaymentNoteService paymentNoteService)
    {
        _paymentNoteService = paymentNoteService;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentNote>>> GetPaymentNote([FromQuery] PaymentNoteFilter filter)
    {
        var dto  = await _paymentNoteService.GetPagedPaymentNotes(filter);
        var count = await _paymentNoteService.GetCountPaymentNotes(filter);

        var  response = dto
            .Select(p => new PaymentNoteResponse(
                p.id,
                p.billId,
                p.date,
                p.amount,
                p.method));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreatePaymentNote([FromBody] PaymentNoteRequest request)
    {
        var (paymentNote, errors) = PaymentNote.Create(
            0,
            request.billId,
            request.date,
            request.amount,
            request.methodId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _paymentNoteService.CreatePaymentNote(paymentNote!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdatePaymentNote(long id, [FromBody] PaymentMethodEnum? method)
    {
        var Id = await _paymentNoteService.UpratePaymentNote(id, method);
        
        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeletePaymentNote(int id)
    {
        var result = await _paymentNoteService.DeletePaymentNote(id);

        return Ok(result);
    }
}
