using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IUserService
{
    Task<long> CreateUser(User user, CancellationToken ct);
    Task<long> DeleteUser(long id, CancellationToken ct);
    Task<UserItem> GetUsersByLogin(string login, CancellationToken ct);
    Task<string> LoginUser(string login, string password, CancellationToken ct);
}