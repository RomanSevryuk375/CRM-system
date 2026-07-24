using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Commands.CancelBill;

public sealed record CancelBillCommand(Guid BillId) : ICommand;
