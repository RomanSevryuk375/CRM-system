using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IUserRepository
{
    Task<int> Create(User user);
    Task<List<User>> Get();
    Task<User> GetByLogin(string login);
}