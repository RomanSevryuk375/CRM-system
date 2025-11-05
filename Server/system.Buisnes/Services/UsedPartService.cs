using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class UsedPartService : IUsedPartService
{
    private readonly IUsedPartRepository _usedPartRepository;
    private readonly ISupplierRepository _supplierRepository;

    public UsedPartService(IUsedPartRepository usedPartRepository, ISupplierRepository supplierRepository)
    {
        _usedPartRepository = usedPartRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<List<UsedPart>> GetUsedPart()
    {
        return await _usedPartRepository.Get();
    }

    public async Task<List<UsedPartWithInfoDto>> GetUsedPartWithInfo()
    {
        var usedParts = await _usedPartRepository.Get();
        var suppliers = await _supplierRepository.Get();

        var response = (from u in usedParts
                        join s in suppliers on u.SupplierId equals s.Id
                        select new UsedPartWithInfoDto(
                            u.Id,
                            u.OrderId,
                            s.Name,
                            u.Name,
                            u.Article,
                            u.Quantity,
                            u.UnitPrice,
                            u.Sum)).ToList();

        return response;
    }

    public async Task<int> CreateUsedPart(UsedPart usedPart)
    {
        return await _usedPartRepository.Create(usedPart);
    }

    public async Task<int> UpdateUsedPart(int id, int orderId, int supplierId, string name, string article, decimal quantity, decimal unitPrice, decimal sum)
    {
        return await _usedPartRepository.Update(id, orderId, supplierId, name, article, quantity, unitPrice, sum);
    }

    public async Task<int> DeleteUsedPart(int id)
    {
        return await _usedPartRepository.Delete(id);
    }
}
