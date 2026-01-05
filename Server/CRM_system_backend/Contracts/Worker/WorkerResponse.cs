namespace CRM_system_backend.Contracts.Worker;

public record WorkerResponse
{
    public int Id { get; init; }
    public long UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public decimal HourlyRate { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};
