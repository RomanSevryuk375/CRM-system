using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Filters;

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
    public async Task<long> CreateClient(Client client, CancellationToken ct)
    {
        return await _decorated.CreateClient(client, ct);
    }

    public async Task<long> CreateClientWithUser(Client client, User user, CancellationToken ct)
    {
        return await _decorated.CreateClientWithUser(client, user, ct);
    }

    public async Task<long> DeleteClient(long id, CancellationToken ct)
    {
        await _distributed.RemoveAsync($"client_{id}", ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteClient(id, ct);
    }

    public async Task<ClientItem> GetClientById(long id, CancellationToken ct)
    {
        var key = $"client_{id}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetClientById(id, ct),
            TimeSpan.FromMinutes(2),
            _logger, ct);
    }

    public async Task<int> GetCountClients(ClientFilter filter, CancellationToken ct)
    {
        return await _decorated.GetCountClients(filter, ct);
    }

    public async Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct)
    {
        return await _decorated.GetPagedClients(filter, ct);
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct)
    {
        await _distributed.RemoveAsync($"client_{id}", ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateClient(id, model, ct);
    }
}
