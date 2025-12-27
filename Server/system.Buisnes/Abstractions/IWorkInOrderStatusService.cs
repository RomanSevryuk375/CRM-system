using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IWorkInOrderStatusService
{
    Task<List<WorkInOrderStatusItem>> GetWiOStatuses();
}