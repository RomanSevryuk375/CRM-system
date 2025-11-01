using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;

    public SpecializationService(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
    }

    public async Task<List<Specialization>> GetSpecialization()
    {
        return await _specializationRepository.Get();
    }

    public async Task<int> CreateSpecialization(Specialization specialization)
    {
        return await _specializationRepository.Create(specialization);
    }

    public async Task<int> UpdateSpecialization(int id, string name)
    {
        return await _specializationRepository.Update(id, name);
    }

    public async Task<int> DeleteSpecialization(int id)
    {
        return await _specializationRepository.Delete(id);
    }
}
