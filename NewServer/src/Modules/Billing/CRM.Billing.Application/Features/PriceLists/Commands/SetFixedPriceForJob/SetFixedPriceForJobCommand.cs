using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Commands.SetFixedPriceForJob;

public sealed record SetFixedPriceForJobCommand(
    Guid PriceListId,
    Guid JobId,
    decimal FixedPrice) : ICommand;