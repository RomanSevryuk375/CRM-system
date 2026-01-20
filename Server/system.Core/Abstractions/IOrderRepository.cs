using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Order;
using Shared.Enums;

namespace CRMSystem.Core.Abstractions;

public interface IOrderRepository
{
    Task<long> Create(Order order, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(OrderFilter filter, CancellationToken ct);
    Task<List<OrderItem>> GetPaged(OrderFilter filter, CancellationToken ct);
    Task<long> Update(long id, OrderPriorityEnum? priorityId, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
    Task<int?> GetStatus(long id, CancellationToken ct);
    Task<long> Complete(long id, CancellationToken ct);
    Task<long> Close(long id, CancellationToken ct);
    Task<bool> PossibleToComplete(long id, CancellationToken ct);
    Task<bool> PossibleToClose(long id, CancellationToken ct);
    Task<OrderItem?> GetByProposalId(long proposalId, CancellationToken ct);
}