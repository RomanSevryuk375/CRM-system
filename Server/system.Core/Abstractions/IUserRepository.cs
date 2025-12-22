using CRMSystem.Core.DTOs.User;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IUserRepository
{
    Task<long> Create(User user);
    Task<long> Delete(long id);
    Task<List<UserItem>> GetByLogin(string login);
    Task<long> Update(long id, UserUpdateModel model);
    Task<bool> Exists(long id);
}