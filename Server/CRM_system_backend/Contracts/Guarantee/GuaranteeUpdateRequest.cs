namespace CRM_system_backend.Contracts.Guarantee;

public record GuaranteeUpdateRequest
(
    string? Description,
    string? Terms
);
