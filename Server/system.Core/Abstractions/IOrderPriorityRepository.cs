using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories;

public interface IOrderPriorityRepository
{
    Task<List<OrderPriorityItem>> Get();
    Task<bool> Exists(int id);
}