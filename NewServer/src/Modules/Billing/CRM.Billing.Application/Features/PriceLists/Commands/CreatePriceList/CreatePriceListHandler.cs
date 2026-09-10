using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.PriceLists.Commands.CreatePriceList;

public sealed class CreatePriceListHandler(IPriceListRepository priceListRepository)
    : ICommandHandler<CreatePriceListCommand>
{
    public async Task<Result> Handle(CreatePriceListCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);
        if (nameResult.IsFailure)
        {
            return nameResult;
        }

        PriceListId id = new(request.PriceListId);
        Result<PriceList> result = PriceList.Create(
            id,
            nameResult.Value,
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
