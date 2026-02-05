// Ignore Spelling: jwt Hasher

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace CRMSystem.Business.Services;

public class UserService(
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IMyPasswordHasher myPasswordHasher,
    IClientRepository clientRepository,
    IWorkerRepository workerRepository,
    ILogger<UserService> logger) : IUserService
{
    public async Task<string> LoginUser(string login, string password, CancellationToken ct)
    {
        logger.LogInformation("Logging user start");

        var user = await GetUsersByLogin(login, ct);

        var result = myPasswordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            logger.LogError("Invalid password for user{userLogin}", login);
            throw new UnauthorizedAccessException("Invalid password");
        }

        var profileId = 0L;
        if (user.RoleId == (int)RoleEnum.Client)
        {
            var clinet = await clientRepository.GetByUserId(user.Id, ct)
                ?? throw new NotFoundException($"Client with user Id{user.Id} not exists");

            profileId = clinet.Id;
        }
        else if (user.RoleId == (int)RoleEnum.Worker)
        {
            var worker = await workerRepository.GetByUserId(user.Id, ct)
                ?? throw new NotFoundException($"Worker with user Id{user.Id} not exists");

            profileId += worker.Id;
        }

            var token = jwtProvider.GenerateToken(user, profileId);

        logger.LogInformation("Logging user success");

        return token;
    }

    public async Task<UserItem> GetUsersByLogin(string login, CancellationToken ct)
    {
        logger.LogInformation("Getting user by login start");

        var user = await userRepository.GetByLogin(login, ct);

        if (user is null)
        {
            logger.LogError("User{userLogin} not found", login);
            throw new NotFoundException($"User {login} not found");
        }

        logger.LogInformation("Getting user by login success");

        return user;
    }

    public async Task<long> CreateUser(User user, CancellationToken ct)
    {
        logger.LogInformation("Creating user start");

        var Id = await userRepository.Create(user, ct);

        logger.LogInformation("Creating user success");

        return Id;
    }

    public async Task<long> DeleteUser(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting user start");

        var Id = await userRepository.Delete(id, ct);

        logger.LogInformation("Deleting user success");

        return Id;
    }
}
