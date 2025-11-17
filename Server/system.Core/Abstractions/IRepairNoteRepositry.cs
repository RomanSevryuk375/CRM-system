using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IRepairNoteRepositry
    {
        Task<List<RepairNote>> Get();
        Task<List<RepairNote>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<RepairNote>> GetByCarId(List<int> carIds);
        Task<List<RepairNote>> GetPagedByCarId(List<int> carIds, int page, int limit);
        Task<int> GetCountByCarId(List<int> carIds);
        Task<List<RepairNote>> GetByOrderId(List<int> orderIds);
        Task<List<RepairNote>> GetPagedByOrderId(List<int> orderIds, int page, int limit);
        Task<int> GetCountByOrderId(List<int> orderIds);
    }
}