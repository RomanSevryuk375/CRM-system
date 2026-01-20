namespace Shared.Contracts.PartSet;

public record PartSetUpdateRequest
(
    decimal? Quantity,
    decimal? SoldPrice
);
