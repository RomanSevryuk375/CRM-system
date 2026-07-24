// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetPriceListById;

public sealed record GetPriceListByIdQuery(Guid PriceListId) : IQuery<PriceListDetailsDto?>;

public sealed record PriceListDetailsDto(
    Guid Id,
    string Name,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    bool IsDefault,
    decimal BaseHourlyRate,
    DateTimeOffset CreatedAt,
    IReadOnlyList<FixedPriceItemDto> FixedPrices);

public sealed record FixedPriceItemDto(
    Guid Id,
    Guid JobId,
    decimal FixedPrice);