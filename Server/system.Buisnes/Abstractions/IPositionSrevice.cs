using CRMSystem.Core.DTOs.Position;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IPositionSrevice
{
    Task<long> CreatePositionWithPart(Position position, Part part);
    Task<long> DeletePosition(long id);
    Task<int> GetCountPositions(PositionFilter filter);
    Task<List<PositionItem>> GetGagedPositions(PositionFilter positionFilter);
    Task<long> UpdatePosition(long id, PositionUpdateModel model);
}