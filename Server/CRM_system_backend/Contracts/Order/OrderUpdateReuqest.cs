using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

public record OrderUpdateReuqest
(
    OrderPriorityEnum priorityId
);
