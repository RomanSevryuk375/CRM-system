using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IOrderPriorityRepository
{
    Task<List<OrderPriorityItem>> Get();
    Task<bool> Exists(int id);
}