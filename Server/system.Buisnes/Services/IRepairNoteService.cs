using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IRepairNoteService
    {
        Task<List<RepairNote>> GetPagedRepairNote(int page, int limit);
        Task<int> GetCountRepairNote();
        Task<List<RepairNoteWithInfoDto>> GetPagedRepairNoteWithInfo(int page, int limit);
        Task<List<RepairNoteWithInfoDto>> GetPagedUserRepairNote(int userId, int page, int limit);
        Task<int> GetCountUserRepairNote(int userId);
        Task<List<RepairNoteWithInfoDto>> GetPagedWorkerRepairNote(int userId, int page, int limit);
        Task<int> GetCountWorkerRepairNote(int userId);
    }
}