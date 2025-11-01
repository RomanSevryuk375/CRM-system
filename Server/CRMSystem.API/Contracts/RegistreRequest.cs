namespace CRM_system_backend.Contracts;

public record RegistreRequest
(
    int RoleId,
    int UserId,
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Login,
    string Password
);
