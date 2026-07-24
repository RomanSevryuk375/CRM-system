// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Queries.GetBillByOrderId;

public sealed record GetBillByOrderIdQuery(Guid OrderId) : IQuery<BillSummaryDto?>;

public sealed record BillSummaryDto(
    Guid Id,
    Guid OrderId,
    int StatusId,
    decimal Amount,
    decimal PaidAmount);