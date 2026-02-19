using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class ClientService(
    IClientRepository clientRepository,
    IUserRepository userRepository,
    IUserContext userContext,
    ILogger<ClientService> logger,
    IUnitOfWork unitOfWork) : IClientService
{
    public async Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting client start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { ClientIds = [userContext.ProfileId] };
        }

        var client = await clientRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting client success");

        return client;
    }

    public async Task<int> GetCountClients(ClientFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count client start");

        var count = await clientRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count client success");

        return count;
    }

    public async Task<ClientItem> GetClientById(long id, CancellationToken ct)
    {
        logger.LogInformation("Getting count client start");

        var client = await clientRepository.GetById(id, ct);
        if (client is null)
        {
            logger.LogError("Client{clientId} not found", id);
            throw new NotFoundException($"Client{id} not found");
        }

        logger.LogInformation("Getting count client success");

        return client;
    }

    public async Task<long> CreateClient(ClientCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating client start");

        if (!await userRepository.Exists(createModel.UserId, ct))
        {
            logger.LogError("User{UserId} not found", createModel.UserId);
            throw new NotFoundException($"User{createModel.UserId} not found");
        }
        
        var (client, errors) = Client.Create(
            0,
            createModel.UserId,
            createModel.Name,
            createModel.Surname,
            createModel.PhoneNumber,
            createModel.Email);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await clientRepository.Create(client!, ct);

        logger.LogInformation("Creating client success");

        return id;
    }

    public async Task<long> CreateClientWithUser(
        ClientCreateModel clientCreateModel,
        UserCreateModel userCreateModel,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            var (user, errorsUser) = User.Create(
                0,
                userCreateModel.RoleId,
                userCreateModel.Login,
                userCreateModel.PasswordHash);

            if (errorsUser is not null && errorsUser.Any())
            {
                throw new ValidationException(string.Join(", ", errorsUser));
            }
            
            logger.LogInformation("Creating user start");

            var userId = await userRepository.Create(user!, ct);
            
            var (client, errorsClient) = Client.Create(
                0,
                userId,
                clientCreateModel.Name,
                clientCreateModel.Surname,
                clientCreateModel.PhoneNumber,
                clientCreateModel.Email);

            if (errorsClient is not null && errorsClient.Any())
            {
                throw new ValidationException(string.Join(", ", errorsClient));
            }

            var id = await clientRepository.Create(client!, ct);

            logger.LogInformation("Creating client success");

            await unitOfWork.CommitTransactionAsync(ct);

            return id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating client start");

        var clientId = await clientRepository.Update(id, model, ct);

        logger.LogInformation("Updating client success");

        return clientId;
    }

    public async Task<long> DeleteClient(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting client start");

        var clientId = await clientRepository.Delete(id, ct);

        logger.LogInformation("Deleting client success");

        return clientId;
    }
}
