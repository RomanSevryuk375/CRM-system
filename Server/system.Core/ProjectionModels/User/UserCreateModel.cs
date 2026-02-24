namespace CRMSystem.Core.ProjectionModels.User;

public record UserCreateModel
(
    int RoleId,
    string Login,
    string PasswordHash);