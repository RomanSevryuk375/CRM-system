using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.PriceLists.Commands.MakePriceListDefault;

public sealed record MakePriceListDefaultCommand(Guid PriceListId) : ICommand;