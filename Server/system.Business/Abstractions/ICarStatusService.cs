using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface ICarStatusService
{
    Task<List<CarStatusItem>> GetCarStatuses(CancellationToken ct);
}