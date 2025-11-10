using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IRepairNoteRepositry
    {
        Task<List<RepairNote>> Get();
        Task<List<RepairNote>> GetByCarId(List<int> carIds);
        Task<List<RepairNote>> GetByOrderId(List<int> orderIds);
    }
}