using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ISpecializationService
    {
        Task<List<Specialization>> GetPagedSpecialization(int page, int limit);
        Task<int> GetCountSpecialization();
        Task<int> CreateSpecialization(Specialization specialization);
        Task<int> DeleteSpecialization(int id);
        Task<int> UpdateSpecialization(int id, string? name);
    }
}