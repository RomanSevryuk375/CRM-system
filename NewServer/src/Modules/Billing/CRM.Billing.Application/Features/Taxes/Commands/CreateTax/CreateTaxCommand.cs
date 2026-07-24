using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Commands.CreateTax;

public sealed record CreateTaxCommand(
    Guid TaxId,
    string Name,
    decimal Rate,
    int TypeId) : ICommand;