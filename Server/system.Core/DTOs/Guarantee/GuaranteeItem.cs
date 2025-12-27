namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeItem
(
    long id,
    long orderId,
    DateOnly dateStart, 
    DateOnly dateEnd, 
    string? description, 
    string terms
);
