namespace Shared.Contracts.Worker;

public record WorkerUpdateRequest
(
    string? Name,
    string? Surname,
    decimal? HourlyRate,
    string? PhoneNumber,
    string? Email
);
