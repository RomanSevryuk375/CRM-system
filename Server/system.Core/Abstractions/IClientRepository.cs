using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IClientRepository
{
    Task<long> Create(Client client, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(ClientFilter filter, CancellationToken ct);
    Task<List<ClientItem>> GetPaged(ClientFilter filter, CancellationToken ct);
    Task<ClientItem?> GetById(long id, CancellationToken ct);
    Task<ClientItem?> GetByUserId(long userId, CancellationToken ct);
    Task<long> Update(long id, ClientUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}