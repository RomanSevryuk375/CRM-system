namespace CRM_system_backend.Contracts.Position;

public record PositionResponse
(
    long id,
    string part,
    long partId,
    int cellId,
    decimal purchasePrice,
    decimal sellingPrice,
    decimal quantity
);
