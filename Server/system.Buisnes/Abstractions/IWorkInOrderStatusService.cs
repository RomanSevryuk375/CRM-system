using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IWorkInOrderStatusService
{
    Task<List<WorkInOrderStatusItem>> GetWiOStatuses();
}