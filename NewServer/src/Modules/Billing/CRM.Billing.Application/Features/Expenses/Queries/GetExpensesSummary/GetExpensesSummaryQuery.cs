// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetExpensesSummary;

public sealed record GetExpensesSummaryQuery(
    DateOnly DateFrom,
    DateOnly DateTo) : IQuery<IReadOnlyList<ExpenseSummaryDto>>;

public sealed record ExpenseSummaryDto(
    int TypeId,
    decimal TotalAmount);