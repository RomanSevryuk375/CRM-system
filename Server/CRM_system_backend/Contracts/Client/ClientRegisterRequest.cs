namespace CRM_system_backend.Contracts.Client;

public record ClientRegisterRequest
(
    long UserId,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    int RoleId,
    string Login,
    string Password
);
