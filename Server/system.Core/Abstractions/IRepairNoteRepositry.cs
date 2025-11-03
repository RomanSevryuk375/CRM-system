using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IRepairNoteRepositry
    {
        Task<List<RepairNote>> Get();
    }
}