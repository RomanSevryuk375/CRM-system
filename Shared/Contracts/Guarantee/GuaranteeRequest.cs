namespace Shared.Contracts.Guarantee;

public record GuaranteeRequest
(
    long OrderId,
    DateOnly DateStart,
    DateOnly DateEnd,
    string? Description,
    string Terms
);
