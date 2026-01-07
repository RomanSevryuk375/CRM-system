using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPartService
{
    Task<long> CreatePart(Part part);
    Task<long> DeletePart(long id);
    Task<int> GetCountParts(PartFilter filter);
    Task<List<PartItem>> GetPagedParts(PartFilter filter);
    Task<long> UpdatePart(long id, PartUpdateModel model);
}