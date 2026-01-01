namespace CRM_system_backend.Contracts.Position;

public record PositionRequest
(
    long partId,
    int cellId,
    decimal purchasePrice,
    decimal sellingPrice,
    decimal quantity
);
