namespace CRM_system_backend.Contracts.PartSet;

public record PartSetResponse
(
    long id,
    long? orderId,
    string position,
    long positionId,
    long? proposalId,
    decimal quantity,
    decimal soldPrice
);
