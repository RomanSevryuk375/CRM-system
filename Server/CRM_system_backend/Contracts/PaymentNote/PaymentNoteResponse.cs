namespace CRM_system_backend.Contracts.PaymentNote;

public record PaymentNoteResponse
(
    long id,
    long billId,
    DateTime date,
    decimal amount,
    string method
);
