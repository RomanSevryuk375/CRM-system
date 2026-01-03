using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderItem
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
