namespace CRMSystem.Core.DTOs.Worker;

public record WorkerUpdateModel
(
    string? Name,
    string? Surname,
    decimal? HourlyRate,
    string? PhoneNumber,
    string? Email
);
