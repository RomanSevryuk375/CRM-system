using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IOrderRepository
{
    Task<long> Create(Order order);
    Task<long> Delete(long id);
    Task<int> GetCount(OrderFilter filter);
    Task<List<OrderItem>> GetPaged(OrderFilter filter);
    Task<long> Update(long id, OrderPriorityEnum? priorityId);
    Task<bool> Exists(long id);
    Task<int?> GetStatus(long id);
    Task<long> Complete(long id);
    Task<long> Close(long id);
    Task<bool> PossibleToComplete(long id);
    Task<bool> PossibleToClose(long id);
    Task<OrderItem?> GetByProposalId(long proposalId);
}