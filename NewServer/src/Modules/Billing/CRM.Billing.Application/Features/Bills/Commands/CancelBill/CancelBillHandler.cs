using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Billing.Application.Features.Bills.Commands.CancelBill;

public sealed class CancelBillHandler(IBillRepository billRepository)
    : IRequestHandler<CancelBillCommand, Result>
{
    public async Task<Result> Handle(CancelBillCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(request.BillId);
        Bill? bill = await billRepository.GetByIdAsync(billId, cancellationToken);
        if (bill is null)
        {
            return Result.Success();
        }

        billRepository.Delete(bill);

        return Result.Success();
    }
}
