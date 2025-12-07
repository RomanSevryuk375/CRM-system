using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class UsedPartRepository : IUsedPartRepository
{
    private readonly SystemDbContext _context;

    public UsedPartRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsedPart>> GetPaged(int page, int limit)
    {
        var usedPartEntities = await _context.UsedParts
            .AsNoTracking()
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var usedPart = usedPartEntities
            .Select(up => UsedPart.Create(
                up.Id,
                up.OrderId,
                up.SupplierId,
                up.Name,
                up.Article,
                up.Quantity,
                up.UnitPrice,
                up.Sum).usedPart)
            .ToList();

        return usedPart;
    }

    public async Task<List<UsedPart>> Get()
    {
        var usedPartEntities = await _context.UsedParts
            .AsNoTracking()
            .ToListAsync();

        var usedPart = usedPartEntities
            .Select(up => UsedPart.Create(
                up.Id,
                up.OrderId,
                up.SupplierId,
                up.Name,
                up.Article,
                up.Quantity,
                up.UnitPrice,
                up.Sum).usedPart)
            .ToList();

        return usedPart;
    }

    public async Task<int> GetCount()
    {
        return await _context.UsedParts.CountAsync();
    }

    public async Task<List<UsedPart>> GetByOrderId(List<int> orderIds)
    {
        var usedPartEntities = await _context.UsedParts
            .AsNoTracking()
            .Where(w => orderIds.Contains(w.OrderId))
            .ToListAsync();

        var usedPart = usedPartEntities
            .Select(up => UsedPart.Create(
                up.Id,
                up.OrderId,
                up.SupplierId,
                up.Name,
                up.Article,
                up.Quantity,
                up.UnitPrice,
                up.Sum).usedPart)
            .ToList();

        return usedPart;
    }

    public async Task<List<UsedPart>> GetByPagedOrderId(List<int> orderIds, int page, int limit)
    {
        var usedPartEntities = await _context.UsedParts
            .AsNoTracking()
            .Where(w => orderIds.Contains(w.OrderId))
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var usedPart = usedPartEntities
            .Select(up => UsedPart.Create(
                up.Id,
                up.OrderId,
                up.SupplierId,
                up.Name,
                up.Article,
                up.Quantity,
                up.UnitPrice,
                up.Sum).usedPart)
            .ToList();

        return usedPart;
    }

    public async Task<int> GetCountByOrderId(List<int> orderIds)
    {
        return await _context.UsedParts.Where(u => orderIds.Contains(u.OrderId)).CountAsync();
    }

    public async Task<int> Create(UsedPart usedPart)
    {
        var (_, error) = UsedPart.Create(
            0,
            usedPart.OrderId,
            usedPart.SupplierId,
            usedPart.Name,
            usedPart.Article,
            usedPart.Quantity,
            usedPart.UnitPrice,
            usedPart.Sum);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Used part: {error}");

        var usedPartEntity = new PartSetEntity
        {
            OrderId = usedPart.OrderId,
            SupplierId = usedPart.SupplierId,
            Name = usedPart.Name,
            Article = usedPart.Article,
            Quantity = usedPart.Quantity,
            UnitPrice = usedPart.UnitPrice,
            Sum = usedPart.Sum
        };

        await _context.UsedParts.AddAsync(usedPartEntity);
        await _context.SaveChangesAsync();

        return usedPartEntity.Id;
    }

    public async Task<int> Update(int id, int? orderId, int? supplierId, string? name, string? article, decimal? quantity, decimal? unitPrice, decimal? sum)
    {
        var usedPart = _context.UsedParts.FirstOrDefault(x => x.Id == id)
              ?? throw new Exception("Used part not found");

        if (orderId.HasValue)
            usedPart.OrderId = orderId.Value;
        if (supplierId.HasValue)
            usedPart.SupplierId = supplierId.Value;
        if (!string.IsNullOrWhiteSpace(name))
            usedPart.Name = name;
        if (!string.IsNullOrWhiteSpace(article))
            usedPart.Article = article;
        if (quantity.HasValue)
            usedPart.Quantity = quantity.Value;
        if (unitPrice.HasValue)
            usedPart.UnitPrice = unitPrice.Value;
        if (sum.HasValue)
            usedPart.Sum = sum.Value;

        await _context.SaveChangesAsync();

        return usedPart.Id;
    }

    public async Task<int> Delete(int id)
    {
        var usedPart = await _context.UsedParts
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
