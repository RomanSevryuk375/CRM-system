using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.PriceLists.Commands.CreatePriceList;

public sealed class CreatePriceListHandler(IPriceListRepository priceListRepository)
    : ICommandHandler<CreatePriceListCommand>
{
    public async Task<Result> Handle(CreatePriceListCommand request, CancellationToken cancellationToken)
    {
        PriceListId id = new(request.PriceListId);
        Result<PriceList> result = PriceList.Create(
            id,
            request.Name,
            request.ValidFrom,
            request.BaseHourlyRate);
        if (result.IsFailure)
        {
            return result;
        }

        await priceListRepository.AddAsync(result.Value, cancellationToken);

        return Result.Success();
    }
}
