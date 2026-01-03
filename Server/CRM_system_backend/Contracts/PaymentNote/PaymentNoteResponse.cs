namespace CRM_system_backend.Contracts.PaymentNote;

public record PaymentNoteResponse
(
    long Id,
    long BillId,
    DateTime Date,
    decimal Amount,
    string Method
);
