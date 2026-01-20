namespace Shared.Contracts.User;

public record UserRequest
(
    int RoleId,
    string Login,
    string Password
);

