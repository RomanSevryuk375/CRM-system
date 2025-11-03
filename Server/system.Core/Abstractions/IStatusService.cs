using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetStatuses();
    }
}