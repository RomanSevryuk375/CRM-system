using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Commands.CloseBillManually;

public sealed record CloseBillManuallyCommand(Guid BillId) : ICommand;
