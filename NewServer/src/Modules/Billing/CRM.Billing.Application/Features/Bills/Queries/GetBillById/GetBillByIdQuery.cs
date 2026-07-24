// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Queries.GetBillById;

public sealed record GetBillByIdQuery(Guid BillId) : IQuery<BillDetailsDto?>;

public sealed record BillDetailsDto(
    Guid Id,
    Guid OrderId,
    int StatusId,
    decimal Amount,
    DateOnly? ActualBillDate,
    DateTimeOffset CreatedAt,
    IReadOnlyList<PaymentNoteDto> Payments);

public sealed record PaymentNoteDto(
    Guid Id,
    decimal Amount,
    int MethodId,
    DateTimeOffset Date);