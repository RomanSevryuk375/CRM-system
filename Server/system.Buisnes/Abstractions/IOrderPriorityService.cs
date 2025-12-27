using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IOrderPriorityService
{
    Task<List<OrderPriorityItem>> GetPrioritys();
}