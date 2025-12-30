namespace CRM_system_backend.Contracts.Acceptance;

public record AcceptanceRequest
(
    long orderId,
    string worker,
    int workerId,
    DateTime createdAt,
    int mileage,
    int fuelLevel,
    string? externalDefects,
    string? internalDefects,
    bool? clientSign,
    bool? workerSign
);
