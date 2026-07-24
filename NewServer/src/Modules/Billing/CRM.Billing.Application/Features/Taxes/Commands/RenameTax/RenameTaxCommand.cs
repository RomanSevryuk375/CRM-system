using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Commands.RenameTax;

public sealed record RenameTaxCommand(
    Guid TaxId,
    string Name) : ICommand;