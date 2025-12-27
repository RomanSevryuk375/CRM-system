using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.PaymentNote;

public record PaymentNoteItem
(
    long id,
    long billId,
    DateTime date,
    decimal amount,
    string method
);
