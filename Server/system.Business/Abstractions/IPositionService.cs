using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPositionService
{
    Task<long> CreatePositionWithPart(Position position, Part part, CancellationToken ct);
    Task<long> DeletePosition(long id, CancellationToken ct);
    Task<int> GetCountPositions(PositionFilter filter, CancellationToken ct);
    Task<List<PositionItem>> GetPagedPositions(PositionFilter positionFilter, CancellationToken ct);
    Task<long> UpdatePosition(long id, PositionUpdateModel model, CancellationToken ct);
}