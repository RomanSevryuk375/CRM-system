using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Commands.DeactivatePriceList;

public sealed record DeactivatePriceListCommand(
    Guid PriceListId,
    DateOnly ValidTo) : ICommand;