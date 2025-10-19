using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> LoginUser(string login, string password)
    {
        return await _userRepository.Login(login, password);
    }

    public async Task<User> GetUsersByLogin(string login)
    {
        return await _userRepository.GetByLogin(login);
    }

    public async Task<int> CreateUser(User user)
    {
        return await _userRepository.Create(user);
    }
}
