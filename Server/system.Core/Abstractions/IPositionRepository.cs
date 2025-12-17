using CRMSystem.Core.DTOs.Position;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPositionRepository
    {
        Task<long> Create(Position position);
        Task<long> Delete(long id);
        Task<int> GetCount(PositionFilter filter);
        Task<List<PositionItem>> GetPaged(PositionFilter filter);
        Task<long> Update(long id, PositionUpdateModel model);
    }
}