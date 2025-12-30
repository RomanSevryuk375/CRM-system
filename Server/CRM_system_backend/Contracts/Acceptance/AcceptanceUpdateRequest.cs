namespace CRM_system_backend.Contracts.Acceptance;

public record AcceptanceUpdateRequest
(
    int? mileage,
    int? fuelLevel,
    string? externalDefects,
    string? internalDefects,
    bool? clientSign,
    bool? workerSign
);
