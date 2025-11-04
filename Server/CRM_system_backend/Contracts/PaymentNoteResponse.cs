namespace CRM_system_backend.Contracts;

public record PaymentNoteResponse
(
    int Id,
    int BillId,
    DateTime Date,
    decimal Amount,
    string Method
);
