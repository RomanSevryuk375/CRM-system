using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface ICarStatusService
{
    Task<List<CarStatusItem>> GetCarStatuses();
}