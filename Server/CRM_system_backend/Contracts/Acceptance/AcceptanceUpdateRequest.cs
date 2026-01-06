namespace CRM_system_backend.Contracts.Acceptance;

public record AcceptanceUpdateRequest
{
    public int? Mileage { get; init; }
    public int? FuelLevel { get; init; }
    public string? ExternalDefects { get; init; }
    public string? InternalDefects { get; init; }
    public bool? ClientSign { get; init; }
    public bool? WorkerSign { get; init; }
};
