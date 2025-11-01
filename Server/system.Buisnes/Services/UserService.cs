using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using CRMSystem.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Buisnes.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMyPasswordHasher _myPasswordHasher;

    public UserService(IUserRepository userRepository, IJwtProvider jwtProvider, IMyPasswordHasher myPasswordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _myPasswordHasher = myPasswordHasher;
    }

    public async Task<string> LoginUser(string login, string password)
    {
        var user = await GetUsersByLogin(login);

        var result = _myPasswordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<User> GetUsersByLogin(string login)
    {
        return await _userRepository.GetByLogin(login);
    }

    public async Task<int> CreateUser(User user)
    {
        return await _userRepository.Create(user);
    }

    public async Task<int> DeleteUser(int id)
    {
        return await _userRepository.Delete(id);
    }
}
