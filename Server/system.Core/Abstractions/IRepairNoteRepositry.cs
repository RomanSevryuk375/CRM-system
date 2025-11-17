using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IRepairNoteRepositry
    {
        Task<List<RepairNote>> Get();
        Task<int> GetCount();
        Task<List<RepairNote>> GetByCarId(List<int> carIds);
        Task<int> GetCountByCarId(List<int> carIds);
        Task<List<RepairNote>> GetByOrderId(List<int> orderIds);
        Task<int> GetCountByOrderId(List<int> orderIds);
    }
}