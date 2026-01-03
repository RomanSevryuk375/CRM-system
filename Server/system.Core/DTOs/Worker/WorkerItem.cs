namespace CRMSystem.Core.DTOs.Worker;

public record WorkerItem
(
    int Id,
    long UserId,
    string Name,
    string Surname,
    decimal HourlyRate,
    string PhoneNumber,
    string Email
);
