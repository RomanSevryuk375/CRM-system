using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
        Task<List<User>> GetUsers();
        Task<User> GetUsersByLogin(string login);
    }
}