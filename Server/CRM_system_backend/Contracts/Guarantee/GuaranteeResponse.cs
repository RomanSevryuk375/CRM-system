namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeResponse
(
    long Id,
    long OrderId,
    DateOnly DateStart,
    DateOnly DateEnd,
    string? Description,
    string Terms
);
