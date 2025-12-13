using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IAbsenceTypeRepository
    {
        Task<int> Create(Core.Models.AbsenceType absenceType);
        Task<int> Delete(int id);
        Task<int> GetCount();
        Task<List<AbcenseTypeItem>> GetPaged(int page, int pageSize);
        Task<int> Update(int id, string name);
    }
}