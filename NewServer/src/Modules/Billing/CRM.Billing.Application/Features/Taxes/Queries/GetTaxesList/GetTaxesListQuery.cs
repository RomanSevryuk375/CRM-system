// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Queries.GetTaxesList;

public sealed record GetTaxesListQuery() : IQuery<IReadOnlyList<TaxListItemDto>>;

public sealed record TaxListItemDto(
    Guid Id,
    string Name,
    decimal Rate,
    int TypeId);