using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRMSystem.Buisnes.Cached;

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

    public async Task<long> DeleteClient(long id)
    {
        await _distributed.RemoveAsync($"client_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteClient(id);
    }

    public async Task<ClientItem> GetClientById(long id)
    {
        var key = $"client_{id}";

        var cachedClient = await _distributed.GetStringAsync(key);

        ClientItem? clientItem;
        if (string.IsNullOrEmpty(cachedClient))
        {
            _logger.LogInformation("Returning client from Db");

            clientItem = await _decorated.GetClientById(id);

            if (clientItem is null)
                return clientItem!;

            await _distributed.SetStringAsync(
                key,
                JsonConvert.SerializeObject(clientItem),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

            _logger.LogInformation("Caching client sucess");

            return clientItem;
        }

        _logger.LogInformation("Returning client from cache");

        clientItem = JsonConvert.DeserializeObject<ClientItem>(cachedClient);

        return clientItem!;
    }

    public async Task<int> GetCountClients(ClientFilter filter)
    {
        return await _decorated.GetCountClients(filter);
    }

    public async Task<List<ClientItem>> GetPagedCkients(ClientFilter filter)
    {
        return await _decorated.GetPagedCkients(filter);
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model)
    {
        await _distributed.RemoveAsync($"client_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateClient(id, model);
    }
}
