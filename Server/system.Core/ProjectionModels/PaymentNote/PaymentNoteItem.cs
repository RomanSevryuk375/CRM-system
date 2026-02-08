using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.PaymentNote;

public record PaymentNoteItem
{
    public long Id { get; init; }
    public long BillId { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public int MethodId { get; init; }
};
