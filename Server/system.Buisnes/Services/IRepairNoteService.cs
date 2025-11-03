using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IRepairNoteService
    {
        Task<List<RepairNote>> GetRepairNote();
        Task<List<RepairNoteWithInfoDto>> GetRepairNoteWithInfo();
    }
}