using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IOrderStatusRepository
{
    Task<List<OrderStatusItem>> Get();
    Task<bool> Exists(int id);
}