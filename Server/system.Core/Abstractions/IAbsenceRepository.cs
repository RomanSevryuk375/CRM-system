using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IAbsenceRepository
    {
        Task<int> Create(Absence absence);
        Task<int> Delete(int id);
        Task<long> GetCount(AbsenceFilter filter);
        Task<List<AbsenceItem>> GetPaged(AbsenceFilter filter);
        Task<int> Update(AbsenceUpdateModel model);
    }
}