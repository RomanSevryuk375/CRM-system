using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.ProjectionModels.Part;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IPositionService
{
    Task<long> CreatePositionWithPart(
        PositionCreateModel positionCreateModel,
        PartCreateModel partCreateModel,
        CancellationToken ct);
    Task<long> DeletePosition(long id, CancellationToken ct);
    Task<int> GetCountPositions(PositionFilter filter, CancellationToken ct);
    Task<List<PositionItem>> GetPagedPositions(PositionFilter positionFilter, CancellationToken ct);
    Task<PositionItem> GetPositionById(int id, CancellationToken ct);
    Task<long> UpdatePosition(long id, PositionUpdateModel model, CancellationToken ct);
}