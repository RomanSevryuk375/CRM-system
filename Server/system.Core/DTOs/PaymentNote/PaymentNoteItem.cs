using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.PaymentNote;

public record PaymentNoteItem
(
    long Id,
    long BillId,
    DateTime Date,
    decimal Amount,
    string Method
);
