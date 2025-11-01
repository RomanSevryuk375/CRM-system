using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ISpecializationService
    {
        Task<int> CreateSpecialization(Specialization specialization);
        Task<int> DeleteSpecialization(int id);
        Task<List<Specialization>> GetSpecialization();
        Task<int> UpdateSpecialization(int id, string name);
    }
}