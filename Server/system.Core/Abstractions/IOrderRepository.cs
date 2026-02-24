using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Order;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IOrderRepository
{
    Task<long> Create(Order order, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(OrderFilter filter, CancellationToken ct);
    Task<OrderDocumentModel?> GetDocumentData(long id, CancellationToken ct);
    Task<List<OrderItem>> GetPaged(OrderFilter filter, CancellationToken ct);
    Task<long> Update(long id, OrderPriorityEnum? priorityId, CancellationToken ct);
    Task<long> PatchOrderFileName(long id, string path, CancellationToken ct);
    Task<long> PatchOrderAgreementFileName(long id, string path, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
    Task<int?> GetStatus(long id, CancellationToken ct);
    Task<long> Complete(long id, CancellationToken ct);
    Task<long> Close(long id, CancellationToken ct);
    Task<bool> PossibleToComplete(long id, CancellationToken ct);
    Task<bool> PossibleToClose(long id, CancellationToken ct);
    Task<OrderItem?> GetByProposalId(long proposalId, CancellationToken ct);
    Task<OrderItem?> GetById(long orderId, CancellationToken ct);
}