namespace CRMSystem.Core.DTOs.PartSet;

public record PartSetUpdateRequest
(
    decimal? Quantity,
    decimal? SoldPrice
);
