using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IUserRepository
{
    Task<long> Create(User user, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<UserItem?> GetByLogin(string login, CancellationToken ct);
    Task<long> Update(long id, UserUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}