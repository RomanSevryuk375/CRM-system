namespace CRM_system_backend.Contracts;

public record PaymentNoteRequest
(
    int? BillId,
    DateTime? Date,
    decimal? Amount,
    string? Method
);
