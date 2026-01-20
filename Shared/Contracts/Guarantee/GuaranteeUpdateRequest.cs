namespace Shared.Contracts.Guarantee;

public record GuaranteeUpdateRequest
(
    string? Description,
    string? Terms
);
