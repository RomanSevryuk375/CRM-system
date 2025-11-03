namespace CRM_system_backend.Contracts;

public record WorkerRegistreRequest
(
    int RoleId,
    int SpecializationId,
    string Name,
    string Surname,
    decimal HourlyRate,
    string Email,
    string PhoneNumber,
    string Login,
    string Password
);
