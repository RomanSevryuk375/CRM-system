using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPartService
{
    Task<long> CreatePart(Part part, CancellationToken ct);
    Task<long> DeletePart(long id, CancellationToken ct);
    Task<int> GetCountParts(PartFilter filter, CancellationToken ct);
    Task<List<PartItem>> GetPagedParts(PartFilter filter, CancellationToken ct);
    Task<long> UpdatePart(long id, PartUpdateModel model, CancellationToken ct);
}