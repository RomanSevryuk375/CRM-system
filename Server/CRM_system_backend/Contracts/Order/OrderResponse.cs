namespace CRMSystem.Core.DTOs.Order;

public record OrderResponse
(
    long Id,
    string Status,
    int StatusId,
    string Car,
    long CarId,
    DateOnly Date,
    string Priority,
    int PriorityId
);
