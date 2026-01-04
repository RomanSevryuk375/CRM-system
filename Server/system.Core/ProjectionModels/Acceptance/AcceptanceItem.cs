namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceItem
{
    public long Id { get; init; }
    public long OrderId { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Mileage { get; init; }
    public int FuelLevel { get; init; }
    public string? ExternalDefects { get; init; }
    public string? InternalDefects { get; init; }
    public bool? ClientSign { get; init; }
    public bool? WorkerSign { get; init; }
};
