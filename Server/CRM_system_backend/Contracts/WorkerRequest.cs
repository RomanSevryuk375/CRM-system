namespace CRM_system_backend.Contracts;

public record WorkerRequest
(
    int UserId,
    int? SpecializationId,
    string? Name,
    string? Surname,
    decimal? HourlyRate,
    string? PhoneNumber,
    string? Email
);
