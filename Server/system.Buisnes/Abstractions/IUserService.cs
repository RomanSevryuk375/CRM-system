using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IUserService
{
    Task<long> CreateUser(User user);
    Task<long> DeleteUser(long id);
    Task<UserItem> GetUsersByLogin(string login);
    Task<string> LoginUser(string login, string password);
}