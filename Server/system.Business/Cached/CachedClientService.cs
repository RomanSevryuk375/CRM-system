using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedClientService : IClientService
{
    private readonly IClientService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedClientService> _logger;

    public CachedClientService(
        IClientService decorated,
        IDistributedCache distributed,
        ILogger<CachedClientService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<long> CreateClient(Client client)
    {
        return await _decorated.CreateClient(client);
    }

    public async Task<long> CreateClientWithUser(Client client, User user)
    {
        return await _decorated.CreateClientWithUser(client, user);
    }

    public async Task<long> DeleteClient(long id)
    {
        await _distributed.RemoveAsync($"client_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteClient(id);
    }

    public async Task<ClientItem> GetClientById(long id)
    {
        var key = $"client_{id}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetClientById(id),
            TimeSpan.FromMinutes(2),
            _logger);
    }

    public async Task<int> GetCountClients(ClientFilter filter)
    {
        return await _decorated.GetCountClients(filter);
    }

    public async Task<List<ClientItem>> GetPagedClients(ClientFilter filter)
    {
        return await _decorated.GetPagedClients(filter);
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model)
    {
        await _distributed.RemoveAsync($"client_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateClient(id, model);
    }
}
