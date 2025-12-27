using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IBillStatusService
{
    Task<List<BillStatusItem>> GetAllBillStatuses();
}