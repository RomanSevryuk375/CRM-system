using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs.User;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using CRMSystem.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMyPasswordHasher _myPasswordHasher;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider, 
        IMyPasswordHasher myPasswordHasher,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _myPasswordHasher = myPasswordHasher;
        _logger = logger;
    }

    public async Task<string> LoginUser(string login, string password)
    {
        _logger.LogInformation("Loggining user start");

        var user = await GetUsersByLogin(login);

        var result = _myPasswordHasher.Verify(password, user.passwordHash);

        if (result == false)
        {
            _logger.LogError("Invalid password for user{userLogin}", login);
            throw new UnauthorizedAccessException("Invalid password");
        }

        var token = _jwtProvider.GenerateToken(user);

        _logger.LogInformation("Loggining user success");

        return token;
    }

    public async Task<UserItem> GetUsersByLogin(string login)
    {
        _logger.LogInformation("Getting user by login start");

        var user = await _userRepository.GetByLogin(login);

        if(user is null)
        {
            _logger.LogError("User{userLogin} not found", login);
            throw new NotFoundException($"User {login} not found");
        }

        _logger.LogInformation("Getting user by login success");

        return user;
    }

    public async Task<long> CreateUser(User user)
    {
        _logger.LogInformation("Creating user start");

        var Id = await _userRepository.Create(user);

        _logger.LogInformation("Creating user success");

        return Id;
    }

    public async Task<long> DeleteUser(long id)
    {
        _logger.LogInformation("Deleting user start");

        var Id = await _userRepository.Delete(id);

        _logger.LogInformation("Deleting user success");

        return Id;
    }
}
