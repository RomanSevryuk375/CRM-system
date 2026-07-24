using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Taxes.Commands.DeleteTax;

public sealed record DeleteTaxCommand(Guid TaxId) : ICommand;