namespace CRM_system_backend.Contracts;

public record UserResponse(
    int Id,
    int RoleId,
    string Login,
    string PasswordHash
    );
