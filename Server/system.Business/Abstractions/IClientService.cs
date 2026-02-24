using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.ProjectionModels.User;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IClientService
{
    Task<long> CreateClient(ClientCreateModel createModel, CancellationToken ct);
    Task<long> DeleteClient(long id, CancellationToken ct);
    Task<ClientItem> GetClientById(long id, CancellationToken ct);
    Task<int> GetCountClients(ClientFilter filter, CancellationToken ct);
    Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct);
    Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct);
    Task<long> CreateClientWithUser(
        ClientCreateModel clientCreateModel,
        UserCreateModel userCreateModel,
        CancellationToken ct);
}