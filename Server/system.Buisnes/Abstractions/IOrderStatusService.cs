using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IOrderStatusService
{
    Task<List<OrderStatusItem>> GetOrderStatuses();
}