using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

public record OrderUpdateRequest
(
    OrderPriorityEnum PriorityId
);
