using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISpecializationRepository
    {
        Task<int> Create(Specialization specialization);
        Task<int> Delete(int id);
        Task<List<Specialization>> Get();
        Task<int> Update(int id, string name);
    }
}