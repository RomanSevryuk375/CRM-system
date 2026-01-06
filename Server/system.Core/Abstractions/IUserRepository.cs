using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IUserRepository
{
    Task<long> Create(User user);
    Task<long> Delete(long id);
    Task<UserItem?> GetByLogin(string login);
    Task<long> Update(long id, UserUpdateModel model);
    Task<bool> Exists(long id);
}