// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetActivePriceList;

public sealed record GetActivePriceListQuery(DateOnly Today) : IQuery<ActivePriceListDto?>;

public sealed record ActivePriceListDto(
    Guid Id,
    string Name,
    decimal BaseHourlyRate,
    IReadOnlyList<ActiveFixedPriceItemDto> FixedPrices);

public sealed record ActiveFixedPriceItemDto(
    Guid JobId,
    decimal FixedPrice);