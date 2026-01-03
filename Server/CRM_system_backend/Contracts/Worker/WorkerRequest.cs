namespace CRM_system_backend.Contracts.Worker;

public record WorkerRequest
(
    long UserId,
    string Name,
    string Surname,
    decimal HourlyRate,
    string PhoneNumber,
    string Email
);
