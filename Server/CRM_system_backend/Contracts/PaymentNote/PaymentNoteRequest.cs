using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.PaymentNote;

public record PaymentNoteRequest
(
    long BillId,
    DateTime Date,
    decimal Amount,
    PaymentMethodEnum MethodId
);
