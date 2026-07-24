using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Commands.CreatePriceList;

public sealed record CreatePriceListCommand(
    Guid PriceListId,
    string Name,
    DateOnly ValidFrom,
    decimal BaseHourlyRate) : ICommand;