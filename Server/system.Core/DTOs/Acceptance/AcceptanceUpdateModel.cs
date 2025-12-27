namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceUpdateModel
(
    int? mileage,
    int? fuelLevel,
    string? externalDefects,
    string? internalDefects,
    bool? clientSign,
    bool? workerSign
);
