namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceUpdateRequest
(
    int? Mileage,
    int? FuelLevel,
    string? ExternalDefects,
    string? InternalDefects,
    bool? ClientSign,
    bool? WorkerSign
);
