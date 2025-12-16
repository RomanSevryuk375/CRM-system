using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<List<OrderStatusItem>> Get();
    }
}