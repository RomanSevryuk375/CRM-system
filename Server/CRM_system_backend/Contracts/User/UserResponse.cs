namespace CRM_system_backend.Contracts.User;

public record UserResponse
(
    long Id,
    string Role,
    int RoleId,
    string Login,
    string PasswordHash
);
