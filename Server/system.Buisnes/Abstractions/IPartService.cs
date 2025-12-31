using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IPartService
{
    Task<long> CreatePart(Part part);
    Task<long> DeletePart(long id);
    Task<int> GetCountParts(PartFilter filter);
    Task<List<PartItem>> GetPagedParts(PartFilter filter);
    Task<long> UpdatePart(long id, PartUpdateModel model);
}