using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class AbsenceTypeService : IAbsenceTypeService
{
    private readonly IAbsenceTypeRepository _absenceTypeRepository;

    public AbsenceTypeService(IAbsenceTypeRepository absenceTypeRepository)
    {
        _absenceTypeRepository = absenceTypeRepository;
    }

    public async Task<List<AbsenceTypeItem>> GetPagedAbsence(int Page, int Limit)
    {
        return await _absenceTypeRepository.GetPaged(Page, Limit);
    }

    public async Task<int> CretaeAbsence(AbsenceType absenceType)
    {
        var repitAbsenceType = await _absenceTypeRepository.GetByName(absenceType.Name);

        if (repitAbsenceType is not null)
            throw new Exception($"Absece type {absenceType.Name} is exist");

        return await _absenceTypeRepository.Create(absenceType);
    }

    public async Task<int> UpdateAbsence(int id, string name)
    {
        return await _absenceTypeRepository.Update(id, name);
    }

    public async Task<int> DeleteAbsence(int id)
    {
        return await _absenceTypeRepository.Delete(id);
    }
}
