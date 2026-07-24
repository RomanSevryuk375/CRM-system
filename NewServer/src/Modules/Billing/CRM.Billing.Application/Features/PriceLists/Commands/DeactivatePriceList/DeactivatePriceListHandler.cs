using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.PriceLists.Commands.DeactivatePriceList;

public sealed class DeactivatePriceListHandler(IPriceListRepository priceListRepository)
    : ICommandHandler<DeactivatePriceListCommand>
{
    public async Task<Result> Handle(DeactivatePriceListCommand request, CancellationToken cancellationToken)
    {
        PriceListId id = new(request.PriceListId);

        PriceList? priceList = await priceListRepository.GetByIdAsync(id, cancellationToken);
        if (priceList is null)
        {
            return Result.Failure(Error.NotFound<PriceList>(
                $"Price list {request.PriceListId} not found."));
        }

        Result result = priceList.Deactivate(request.ValidTo);
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}
