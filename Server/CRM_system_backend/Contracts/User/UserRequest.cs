namespace CRM_system_backend.Contracts.User;

public record UserRequest
(
    int RoleId,
    string Login,
    string Password
);

