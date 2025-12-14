using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IBillStatusRepository
    {
        Task<List<BillStatusItem>> Get();
    }
}