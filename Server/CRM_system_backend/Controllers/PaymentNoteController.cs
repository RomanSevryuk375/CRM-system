using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PaymentNote;
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
    public async Task<ActionResult<List<PaymentNoteResponse>>> GetPaymentNote(
        [FromQuery] PaymentNoteFilter filter, CancellationToken ct)
    {
        var dto  = await paymentNoteService.GetPagedPaymentNotes(filter, ct);
        var count = await paymentNoteService.GetCountPaymentNotes(filter, ct);

        var  response = mapper.Map<List<PaymentNoteResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<PaymentNoteResponse>> GetPaymentNoteById(
        long id, CancellationToken ct)
    {
        var dto  = await paymentNoteService.GetPaymentNoteById(id, ct);
        var  response = mapper.Map<PaymentNoteResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreatePaymentNote(
        [FromBody] PaymentNoteRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<PaymentNoteCreateModel>(request);
        var id = await paymentNoteService.CreatePaymentNote(createModel, ct);
        
        var createdDto = await paymentNoteService.GetPaymentNoteById(id, ct);
        var response = mapper.Map<PaymentNoteResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetPaymentNoteById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePaymentNote(
        long id, [FromBody] PaymentMethodEnum? method, CancellationToken ct)
    {
        await paymentNoteService.UpratePaymentNote(id, method, ct);
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePaymentNote(int id, CancellationToken ct)
    {
        await paymentNoteService.DeletePaymentNote(id, ct);

        return NoContent();
    }
}
