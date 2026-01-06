namespace CRMSystem.Core.ProjectionModels.User;

public record UserItem
(
    long Id,
    string Role,
    int RoleId,
    string Login,
    string PasswordHash
);

