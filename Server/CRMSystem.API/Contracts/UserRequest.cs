namespace CRM_system_backend.Contracts;

public record UserRequest(
    int RoleId,
    string Login,
    string Password
    );

