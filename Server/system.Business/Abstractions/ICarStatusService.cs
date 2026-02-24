using CRMSystem.Core.ProjectionModels.CarStatus;

namespace CRMSystem.Business.Abstractions;

public interface ICarStatusService
{
    Task<List<CarStatusItem>> GetCarStatuses(CancellationToken ct);
    Task<CarStatusItem> GetCarStatusById(int id, CancellationToken ct);
}