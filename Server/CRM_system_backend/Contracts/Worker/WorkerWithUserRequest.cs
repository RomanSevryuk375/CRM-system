namespace CRM_system_backend.Contracts.Worker;

public record WorkerWithUserRequest
(
    string Name,
    string Surname,
    decimal HourlyRate,
    string PhoneNumber,
    string Email,
    int RoleId,
    string Login,
    string Password
);
