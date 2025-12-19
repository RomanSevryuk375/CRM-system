namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceItem
(
    long id,
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
