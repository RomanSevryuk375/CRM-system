namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeItem
(
    long Id,
    long OrderId,
    DateOnly DateStart,
    DateOnly DateEnd,
    string? Description,
    string Terms
);
