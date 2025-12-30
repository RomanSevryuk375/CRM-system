namespace CRM_system_backend.Contracts.Guarantee;

public record GuaranteeRequest
(
    long orderId,
    DateOnly dateStart,
    DateOnly dateEnd,
    string? description,
    string terms
);
