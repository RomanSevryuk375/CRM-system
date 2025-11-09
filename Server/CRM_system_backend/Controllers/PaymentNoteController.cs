using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentNoteController : ControllerBase
{
    private readonly IPaymentNoteService _paymentNoteService;

    public PaymentNoteController(IPaymentNoteService paymentNoteService)
    {
        _paymentNoteService = paymentNoteService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<PaymentNote>>> GetPaymentNote()
    {
        var paymentNotes  = await _paymentNoteService.GetPaymentNote();

        var  response = paymentNotes
            .Select(p => new PaymentNoteResponse(
                p.Id,
                p.BillId,
                p.Date,
                p.Amount,
                p.Method))
            .ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    [Authorize(Policy = "UserPolicy")]

    public async Task<ActionResult<int>> CreatePaymentNote([FromBody] PaymentNoteRequest paymentNoteRequest)
    {
        var (paymentNote, error) = PaymentNote.Create(
            0,
            paymentNoteRequest.BillId,
            paymentNoteRequest.Date,
            paymentNoteRequest.Amount,
            paymentNoteRequest.Method);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var paymentNoteId = await _paymentNoteService.CreatePaymentNote(paymentNote);

        return Ok(paymentNoteId);
    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdatePaymentNote([FromBody] PaymentNoteRequest paymentNoteRequest, int id)
    {
        var result = await _paymentNoteService.UpdatePaymentNote(
            id,
            paymentNoteRequest.BillId,
            paymentNoteRequest.Date,
            paymentNoteRequest.Amount,
            paymentNoteRequest.Method);
        
        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> DeletePaymentNote(int id)
    {
        var result = await _paymentNoteService.DeletePaymentNote(id);

        return Ok(result);
    }

    //user can get it by id from jwt
    //[Authorize(Policy = "UserPolicy")]
}
