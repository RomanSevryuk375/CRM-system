using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Taxes.Commands.UpdateTaxRate;

internal sealed class UpdateTaxRateHandler(ITaxRepository taxRepository)
    : ICommandHandler<UpdateTaxRateCommand>
{
    public async Task<Result> Handle(UpdateTaxRateCommand request, CancellationToken cancellationToken)
    {
        TaxId taxId = new(request.TaxId);
        Tax? tax = await taxRepository.GetByIdAsync(taxId, cancellationToken);
        if (tax is null)
        {
            return Result.Failure(Error.NotFound<Tax>(
                "The specified tax was not found."));
        }

        Result result = tax.UpdateRate(request.Rate);
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}