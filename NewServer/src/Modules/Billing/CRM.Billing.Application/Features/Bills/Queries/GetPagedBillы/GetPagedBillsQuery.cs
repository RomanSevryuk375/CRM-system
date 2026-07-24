// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Queries.GetPagedBillы;

public sealed record GetPagedBillsQuery(
    int Limit,
    int Offset,
    Guid? OrderId = null,
    int? StatusId = null) : IQuery<IReadOnlyList<BillListItemDto>>;

public sealed record BillListItemDto(
    Guid Id,
    Guid OrderId,
    int StatusId,
    decimal Amount,
    DateTimeOffset CreatedAt);