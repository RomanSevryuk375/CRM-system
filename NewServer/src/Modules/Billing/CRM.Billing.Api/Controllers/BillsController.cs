using CRM.Billing.Api.DTOs;
using CRM.Billing.Application.Features.Bills.Commands.AddPaymentNote;
using CRM.Billing.Application.Features.Bills.Commands.CancelBill;
using CRM.Billing.Application.Features.Bills.Commands.CloseBillManually;
using CRM.Billing.Application.Features.Bills.Commands.CreateBill;
using CRM.Billing.Application.Features.Bills.Commands.RemovePaymentNote;
using CRM.Billing.Application.Features.Bills.Queries.GetBillById;
using CRM.Billing.Application.Features.Bills.Queries.GetBillByOrderId;
using CRM.Billing.Application.Features.Bills.Queries.GetPagedBillы;
using CRM.Shared.Abstractions.Results;
using CRM.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Billing.Api.Controllers;

[Route("api/v1/bills")]
public sealed class BillsController(ISender sender) : ApiController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetBillByIdQuery query = new(id);
        BillDetailsDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int limit = 50,
        [FromQuery] int offset = 0,
        [FromQuery] Guid? orderId = null,
        [FromQuery] int? statusId = null,
        CancellationToken cancellationToken = default)
    {
        GetPagedBillsQuery query = new(limit, offset, orderId, statusId);
        IReadOnlyList<BillListItemDto> response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("by-order/{orderId:guid}")]
    public async Task<IActionResult> GetByOrderId(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        GetBillByOrderIdQuery query = new(orderId);
        BillSummaryDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBillCommand command,
        CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPost("{id:guid}/payments")]
    public async Task<IActionResult> AddPayment(
        Guid id,
        [FromBody] AddPaymentRequest request,
        CancellationToken cancellationToken)
    {
        AddPaymentNoteCommand command = new(
            id,
            request.PaymentId,
            request.Amount,
            request.MethodId,
            request.PaymentDate);

        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpDelete("{id:guid}/payments/{paymentId:guid}")]
    public async Task<IActionResult> RemovePayment(Guid id, Guid paymentId, CancellationToken cancellationToken)
    {
        RemovePaymentNoteCommand command = new(id, paymentId);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> CloseManually(Guid id, CancellationToken cancellationToken)
    {
        CloseBillManuallyCommand command = new(id);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        CancelBillCommand command = new(id);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }
}
