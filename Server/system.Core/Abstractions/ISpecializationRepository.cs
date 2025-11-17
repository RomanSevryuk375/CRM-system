using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISpecializationRepository
    {
        Task<List<Specialization>> Get();
        Task<int> GetCount();
        Task<int> Create(Specialization specialization);
        Task<int> Delete(int id);
        Task<int> Update(int id, string name);
    }
}