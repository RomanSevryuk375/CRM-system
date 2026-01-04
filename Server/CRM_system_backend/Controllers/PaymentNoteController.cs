using AutoMapper;
using CRM_system_backend.Contracts.PaymentNote;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<PaymentNote>>> GetPaymentNote([FromQuery] PaymentNoteFilter filter)
    {
        var dto  = await _paymentNoteService.GetPagedPaymentNotes(filter);
        var count = await _paymentNoteService.GetCountPaymentNotes(filter);

        var  response = _mapper.Map<List<PaymentNoteResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreatePaymentNote([FromBody] PaymentNoteRequest request)
    {
        var (paymentNote, errors) = PaymentNote.Create(
            0,
            request.BillId,
            request.Date,
            request.Amount,
            request.MethodId);

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
