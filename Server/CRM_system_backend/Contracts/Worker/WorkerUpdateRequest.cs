namespace CRM_system_backend.Contracts.Worker;

public record WorkerUpdateRequest
(
    string? name,
    string? surname,
    decimal? hourlyRate,
    string? phoneNumber,
    string? email
);
