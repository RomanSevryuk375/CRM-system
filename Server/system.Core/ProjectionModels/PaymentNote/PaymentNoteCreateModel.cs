using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.PaymentNote;

public record PaymentNoteCreateModel
(
    long BillId,
    DateTime Date,
    decimal Amount,
    PaymentMethodEnum MethodId);