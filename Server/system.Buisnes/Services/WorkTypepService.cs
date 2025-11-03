using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkTypepService : IWorkTypepService
{
    private readonly IWorkTypeRepository _workTypeRepository;

    public WorkTypepService(IWorkTypeRepository workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<List<WorkType>> GetWorkType()
    {
        return await _workTypeRepository.Get();
    }

    public async Task<int> CreateWorkType(WorkType workType)
    {
        return await _workTypeRepository.Create(workType);
    }

    public async Task<int> UpdateWorkType(int id, string title, string category, string description, decimal standardTime)
    {
        return await _workTypeRepository.Update(id, title, category, description, standardTime);
    }

    public async Task<int> DeleteWorkType(int id)
    {
        return await _workTypeRepository.Delete(id);
    }
}
