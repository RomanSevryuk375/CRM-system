using Shared.Enums;

namespace Shared.Contracts.PaymentNote;

public record PaymentNoteRequest
(
    long BillId,
    DateTime Date,
    decimal Amount,
    PaymentMethodEnum MethodId
);
