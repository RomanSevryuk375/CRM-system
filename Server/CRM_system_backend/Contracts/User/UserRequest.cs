namespace CRM_system_backend.Contracts.User;

public record UserRequest
(
    int roleId,
    string login,
    string password
);

