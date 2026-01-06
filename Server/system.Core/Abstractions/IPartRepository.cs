using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPartRepository
{
    Task<long> Create(Part part);
    Task<long> Delete(long id);
    Task<int> GetCount(PartFilter filter);
    Task<List<PartItem>> GetPaged(PartFilter filter);
    Task<long> Update(long id, PartUpdateModel model);
    Task<bool> Exists(long id);
}