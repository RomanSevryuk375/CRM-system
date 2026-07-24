// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Queries.GetTaxById;

public sealed record GetTaxByIdQuery(Guid TaxId) : IQuery<TaxDetailsDto?>;

public sealed record TaxDetailsDto(
    Guid Id,
    string Name,
    decimal Rate,
    int TypeId,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);