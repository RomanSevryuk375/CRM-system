using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Taxes.Commands.DeleteTax;

public sealed class DeleteTaxHandler(ITaxRepository taxRepository)
    : ICommandHandler<DeleteTaxCommand>
{
    public async Task<Result> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
    {
        TaxId taxId = new(request.TaxId);
        Tax? tax = await taxRepository.GetByIdAsync(taxId, cancellationToken);
        if (tax is null)
        {
            return Result.Success();
        }

        taxRepository.Delete(tax);

        return Result.Success();
    }
}