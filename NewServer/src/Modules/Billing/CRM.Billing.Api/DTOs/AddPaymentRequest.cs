namespace CRM.Billing.Api.DTOs;

public sealed record AddPaymentRequest(
    Guid PaymentId,
    decimal Amount,
    int MethodId,
    DateTimeOffset PaymentDate);