using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IClientService
{
    Task<long> CreateClient(Client client, CancellationToken ct);
    Task<long> DeleteClient(long id, CancellationToken ct);
    Task<ClientItem> GetClientById(long id, CancellationToken ct);
    Task<int> GetCountClients(ClientFilter filter, CancellationToken ct);
    Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct);
    Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct);
    Task<long> CreateClientWithUser(Client client, User user, CancellationToken ct);
}