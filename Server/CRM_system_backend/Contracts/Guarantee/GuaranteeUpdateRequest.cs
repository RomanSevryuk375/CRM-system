namespace CRM_system_backend.Contracts.Guarantee;

public record GuaranteeUpdateRequest
(
    string? description,
    string? terms
);
