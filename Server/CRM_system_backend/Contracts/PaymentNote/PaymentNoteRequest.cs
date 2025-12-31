using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.PaymentNote;

public record PaymentNoteRequest
(
    long billId,
    DateTime date,
    decimal amount,
    PaymentMethodEnum methodId
);
