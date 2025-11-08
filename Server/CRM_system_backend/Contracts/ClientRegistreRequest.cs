namespace CRM_system_backend.Contracts;

public record ClientRegistreRequest
(
    int RoleId,
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Login,
    string Password
);
