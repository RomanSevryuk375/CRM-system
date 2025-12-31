namespace CRM_system_backend.Contracts.PartSet;

public record PartSetRequest
(
    long? orderId,
    long positionId,
    long? proposalId,
    decimal quantity,
    decimal soldPrice
);
