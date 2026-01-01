namespace CRM_system_backend.Contracts.Worker;

public record WorkerResponse
(
    int id,
    long userId,
    string name,
    string surname,
    decimal hourlyRate,
    string phoneNumber,
    string email
);
