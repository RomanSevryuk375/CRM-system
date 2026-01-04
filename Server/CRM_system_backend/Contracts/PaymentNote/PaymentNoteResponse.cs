namespace CRM_system_backend.Contracts.PaymentNote;

public record PaymentNoteResponse
{
    public long Id { get; init; }
    public long BillId { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public string Method { get; init; } = string.Empty;
};
