namespace CRM_system_backend.Contracts.PartSet;

public record PartSetUpdateRequest
(
    decimal? quantity,
    decimal? soldPrice
);
