namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceItem
(
    long Id,
    long OrderId,
    string Worker,
    int WorkerId,
    DateTime CreatedAt,
    int Mileage,
    int FuelLevel,
    string? ExternalDefects,
    string? InternalDefects,
    bool? ClientSign,
    bool? WorkerSign
);
