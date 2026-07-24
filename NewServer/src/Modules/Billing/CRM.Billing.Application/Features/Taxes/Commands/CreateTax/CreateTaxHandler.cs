using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Taxes.Commands.CreateTax;

public sealed class CreateTaxHandler(ITaxRepository taxRepository)
    : ICommandHandler<CreateTaxCommand>
{
    public async Task<Result> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
    {
        TaxId taxId = new(request.TaxId);
        TaxType taxType = (TaxType)request.TypeId;
        Result<Tax> result = Tax.Create(taxId, request.Name, request.Rate, taxType);
        if (result.IsFailure)
        {
            return result;
        }

        await taxRepository.AddAsync(result.Value, cancellationToken);

        return Result.Success();
    }
}