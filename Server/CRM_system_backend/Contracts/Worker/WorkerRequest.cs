namespace CRM_system_backend.Contracts.Worker;

public record WorkerRequest
(
    long userId,
    string name,
    string surname,
    decimal hourlyRate,
    string phoneNumber,
    string email
);
