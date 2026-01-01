namespace CRM_system_backend.Contracts.Position;

public record PositionUpdateRequest
(
    int? cellId,
    decimal? purchasePrice,
    decimal? sellingPrice,
    decimal? quantity
);
