// Ignore Spelling: jwt Hasher

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace CRMSystem.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMyPasswordHasher _myPasswordHasher;
    private readonly IClientRepository _clientRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IMyPasswordHasher myPasswordHasher,
        IClientRepository clientRepository,
        IWorkerRepository workerRepository,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _myPasswordHasher = myPasswordHasher;
        _clientRepository = clientRepository;
        _workerRepository = workerRepository;
        _logger = logger;
    }

    public async Task<string> LoginUser(string login, string password, CancellationToken ct)
    {
        _logger.LogInformation("Logging user start");

        var user = await GetUsersByLogin(login, ct);

        var result = _myPasswordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            _logger.LogError("Invalid password for user{userLogin}", login);
            throw new UnauthorizedAccessException("Invalid password");
        }

        var profileId = 0L;
        if (user.RoleId == (int)RoleEnum.Client)
        {
            var clinet = await _clientRepository.GetByUserId(user.Id, ct)
                ?? throw new NotFoundException($"Client with user Id{user.Id} not exists");

            profileId = clinet.Id;
        }
        else if (user.RoleId == (int)RoleEnum.Worker)
        {
            var worker = await _workerRepository.GetByUserId(user.Id, ct)
                ?? throw new NotFoundException($"Worker with user Id{user.Id} not exists");

            profileId += worker.Id;
        }

            var token = _jwtProvider.GenerateToken(user, profileId);

        _logger.LogInformation("Logging user success");

        return token;
    }

    public async Task<UserItem> GetUsersByLogin(string login, CancellationToken ct)
    {
        _logger.LogInformation("Getting user by login start");

        var user = await _userRepository.GetByLogin(login, ct);

        if (user is null)
        {
            _logger.LogError("User{userLogin} not found", login);
            throw new NotFoundException($"User {login} not found");
        }

        _logger.LogInformation("Getting user by login success");

        return user;
    }

    public async Task<long> CreateUser(User user, CancellationToken ct)
    {
        _logger.LogInformation("Creating user start");

        var Id = await _userRepository.Create(user, ct);

        _logger.LogInformation("Creating user success");

        return Id;
    }

    public async Task<long> DeleteUser(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting user start");

        var Id = await _userRepository.Delete(id, ct);

        _logger.LogInformation("Deleting user success");

        return Id;
    }
}
