using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISpecializationRepository
    {
        Task<int> Create(Specialization specialization);
        Task<int> Delete(int id);
        Task<List<SpecializationItem>> Get();
        Task<int> Update(int id, string? name);
    }
}