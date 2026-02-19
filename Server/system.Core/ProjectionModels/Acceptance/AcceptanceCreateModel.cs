namespace CRMSystem.Core.ProjectionModels.Acceptance;

public record AcceptanceCreateModel
(
    long Id,
    long OrderId,
    int WorkerId,
    DateTime CreatedAt,
    int Mileage,
    int FuelLevel,
    string? ExternalDefects,
    string? InternalDefects,
    bool? ClientSign,
    bool? WorkerSign);