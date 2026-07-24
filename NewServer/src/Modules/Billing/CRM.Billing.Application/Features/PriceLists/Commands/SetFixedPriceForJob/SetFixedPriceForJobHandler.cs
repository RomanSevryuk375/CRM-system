using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;
using MassTransit;

namespace CRM.Billing.Application.Features.PriceLists.Commands.SetFixedPriceForJob;

public sealed class SetFixedPriceForJobHandler(IPriceListRepository priceListRepository)
    : ICommandHandler<SetFixedPriceForJobCommand>
{
    public async Task<Result> Handle(SetFixedPriceForJobCommand request, CancellationToken cancellationToken)
    {
        PriceListId id = new(request.PriceListId);
        PriceList? priceList = await priceListRepository.GetByIdAsync(id, cancellationToken);
        if (priceList is null)
        {
            return Result.Failure(Error.NotFound<PriceList>(
                $"Price list {request.PriceListId} not found."));
        }

        PriceListItemId listItemId = new(NewId.NextGuid());
        JobId jobId = new(request.JobId);

        Result result = priceList.SetFixedPriceForJob(
            listItemId, jobId, request.FixedPrice);
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}
