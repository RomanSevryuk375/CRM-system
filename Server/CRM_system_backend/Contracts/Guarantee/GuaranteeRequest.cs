namespace CRM_system_backend.Contracts.Guarantee;

public record GuaranteeRequest
(
    long OrderId,
    DateOnly DateStart,
    DateOnly DateEnd,
    string? Description,
    string Terms
);
