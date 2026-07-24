namespace CRM.Billing.Api.DTOs;

public sealed record SetFixedPriceRequest(
    Guid JobId,
    decimal FixedPrice);
