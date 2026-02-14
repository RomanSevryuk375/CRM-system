using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Client;
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

    public async Task<long> CreateClient(Client client, CancellationToken ct)
    {
        logger.LogInformation("Creating client start");

        if (!await userRepository.Exists(client.UserId, ct))
        {
            logger.LogError("User{UserId} not found", client.UserId);
            throw new NotFoundException($"User{client.UserId} not found");
        }

        var Id = await clientRepository.Create(client, ct);

        logger.LogInformation("Creating client success");

        return Id;
    }

    public async Task<long> CreateClientWithUser(Client client, User user, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating user start");

            var userId = await userRepository.Create(user, ct);
            client.SetUserId(userId);

            var Id = await clientRepository.Create(client, ct);

            logger.LogInformation("Creating client success");

            await unitOfWork.CommitTransactionAsync(ct);

            return Id;
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

        var Id = await clientRepository.Update(id, model, ct);

        logger.LogInformation("Updating client success");

        return Id;
    }

    public async Task<long> DeleteClient(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting client start");

        var Id = await clientRepository.Delete(id, ct);

        logger.LogInformation("Deleting client success");

        return Id;
    }
}
