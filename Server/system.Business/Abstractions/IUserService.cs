using CRMSystem.Core.ProjectionModels.User;

namespace CRMSystem.Business.Abstractions;

public interface IUserService
{
    Task<long> CreateUser(UserCreateModel createModel, CancellationToken ct);
    Task<long> DeleteUser(long id, CancellationToken ct);
    Task<UserItem> GetUsersByLogin(string login, CancellationToken ct);
    Task<UserItem> GetUserById(long id, CancellationToken ct);
    Task<string> LoginUser(string login, string password, CancellationToken ct);
}