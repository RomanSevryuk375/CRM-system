namespace CRMSystem.Core.ProjectionModels.Worker;

public record WorkerCreateModel
(
    long UserId,
    string Name,
    string Surname,
    decimal HourlyRate,
    string PhoneNumber,
    string Email);