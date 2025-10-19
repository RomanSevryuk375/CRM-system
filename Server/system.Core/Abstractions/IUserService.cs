using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
        Task<string> LoginUser(string login, string password);
        Task<User> GetUsersByLogin(string login);
    }
}