using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPositionService
{
    Task<long> CreatePositionWithPart(Position position, Part part);
    Task<long> DeletePosition(long id);
    Task<int> GetCountPositions(PositionFilter filter);
    Task<List<PositionItem>> GetPagedPositions(PositionFilter positionFilter);
    Task<long> UpdatePosition(long id, PositionUpdateModel model);
}