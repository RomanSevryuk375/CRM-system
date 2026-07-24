using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Commands.UpdateTaxRate;

public sealed record UpdateTaxRateCommand(
    Guid TaxId,
    decimal Rate) : ICommand;