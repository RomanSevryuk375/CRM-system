namespace CRM_system_backend.Contracts.User;

public record UserResponse
(
    long id,
    string role,
    int roleId,
    string login,
    string passwordHash
);
