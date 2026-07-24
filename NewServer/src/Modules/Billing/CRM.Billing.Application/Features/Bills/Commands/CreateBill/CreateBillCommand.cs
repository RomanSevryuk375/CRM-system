using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Commands.CreateBill;

public sealed record CreateBillCommand(
    Guid BillId,
    Guid OrderId,
    int StatusId,
    decimal Amount,
    DateOnly? ActualBillDate) : ICommand;
