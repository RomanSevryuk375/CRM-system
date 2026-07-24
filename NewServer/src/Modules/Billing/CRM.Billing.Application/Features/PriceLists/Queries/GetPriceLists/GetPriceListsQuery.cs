// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetPriceLists;

public sealed record GetPriceListsQuery(
    int Limit = 50,
    int Offset = 0) : IQuery<IReadOnlyList<PriceListSummaryDto>>;

public sealed record PriceListSummaryDto(
    Guid Id,
    string Name,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsDefault,
    decimal BaseHourlyRate,
    DateTimeOffset CreatedAt);