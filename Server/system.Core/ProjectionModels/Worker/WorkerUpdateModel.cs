namespace CRMSystem.Core.ProjectionModels.Worker;

public record WorkerUpdateModel
(
    string? Name,
    string? Surname,
    decimal? HourlyRate,
    string? PhoneNumber,
    string? Email
);
