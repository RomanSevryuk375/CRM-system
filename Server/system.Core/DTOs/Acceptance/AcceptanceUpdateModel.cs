namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceUpdateModel
(
    int? Mileage,
    int? FuelLevel,
    string? ExternalDefects,
    string? InternalDefects,
    bool? ClientSign,
    bool? WorkerSign
);
