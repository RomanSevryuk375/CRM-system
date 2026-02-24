using CRMSystem.Core.ProjectionModels.Part;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IPartService
{
    Task<long> CreatePart(PartCreateModel createModel, CancellationToken ct);
    Task<long> DeletePart(long id, CancellationToken ct);
    Task<int> GetCountParts(PartFilter filter, CancellationToken ct);
    Task<List<PartItem>> GetPagedParts(PartFilter filter, CancellationToken ct);
    Task<PartItem> GetPartById(long id, CancellationToken ct);
    Task<long> UpdatePart(long id, PartUpdateModel model, CancellationToken ct);
}