using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkTypeService : IWorkTypeService
{
    private readonly IWorkTypeRepository _workTypeRepository;

    public WorkTypeService(IWorkTypeRepository workTypeRepository)
    {
        _workTypeRepository = workTypeRepository;
    }

    public async Task<List<WorkType>> GetPagedWorkType(int page, int limit)
    {
        var workType = await _workTypeRepository.Get();
                             
        var pagedWorkTypes = workType
                            .Skip((page - 1) * limit)
                            .Take(limit)
                            .ToList();

        return pagedWorkTypes;
    }

    public async Task<int> GetWorkTypeCount()
    {
        return await _workTypeRepository.GetCount();
    }

    public async Task<int> CreateWorkType(WorkType workType)
    {
        return await _workTypeRepository.Create(workType);
    }

    public async Task<int> UpdateWorkType(int id, string? title, string? category, string? description, decimal? standardTime)
    {
        return await _workTypeRepository.Update(id, title, category, description, standardTime);
    }

    public async Task<int> DeleteWorkType(int id)
    {
        return await _workTypeRepository.Delete(id);
    }
}
