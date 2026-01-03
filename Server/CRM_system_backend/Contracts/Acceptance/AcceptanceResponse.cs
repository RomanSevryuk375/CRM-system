namespace CRM_system_backend.Contracts.Acceptance;

public record AcceptanceResponse
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
