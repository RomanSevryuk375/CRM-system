using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Cached;

public class CachedClientService(
    IClientService decorated,
    IDistributedCache distributed,
    ILogger<CachedClientService> logger) : IClientService
{
    public async Task<long> CreateClient(Client client, CancellationToken ct)
    {
        return await decorated.CreateClient(client, ct);
    }

    public async Task<long> CreateClientWithUser(Client client, User user, CancellationToken ct)
    {
        return await decorated.CreateClientWithUser(client, user, ct);
    }

    public async Task<long> DeleteClient(long id, CancellationToken ct)
    {
        await distributed.RemoveAsync($"client_{id}", ct);

        logger.LogInformation("Removing cache success");

        return await decorated.DeleteClient(id, ct);
    }

    public async Task<ClientItem> GetClientById(long id, CancellationToken ct)
    {
        var key = $"client_{id}";

        return await distributed.GetOrCreateAsync(
            key,
            () => decorated.GetClientById(id, ct),
            TimeSpan.FromMinutes(2),
            logger, ct);
    }

    public async Task<int> GetCountClients(ClientFilter filter, CancellationToken ct)
    {
        return await decorated.GetCountClients(filter, ct);
    }

    public async Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct)
    {
        return await decorated.GetPagedClients(filter, ct);
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct)
    {
        await distributed.RemoveAsync($"client_{id}", ct);

        logger.LogInformation("Removing cache success");

        return await decorated.UpdateClient(id, model, ct);
    }
}
