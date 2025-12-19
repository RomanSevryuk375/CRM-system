using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IAbsenceTypeRepository
{
    Task<int> Create(AbsenceType absenceType);
    Task<int> Delete(int id);
    Task<int> GetCount();
    Task<List<AbsenceTypeItem>> GetPaged(int page, int pageSize);
    Task<List<AbsenceTypeItem>> GetByName(string name);
    Task<int> Update(int id, string name);
}