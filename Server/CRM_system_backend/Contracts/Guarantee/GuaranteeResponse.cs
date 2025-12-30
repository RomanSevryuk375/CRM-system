namespace CRM_system_backend.Contracts.Guarantee;

public record GuaranteeResponse
(
    long id,
    long orderId,
    DateOnly dateStart,
    DateOnly dateEnd,
    string? description,
    string terms
);
