// Ignore Spelling: Img

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.ProjectionModels.AccetanceImg;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class AcceptanceImgRepository(
    SystemDbContext context,
    IMapper mapper) : IAcceptanceImgRepository
{
    private static IQueryable<AcceptanceImgEntity> ApplyFilter(IQueryable<AcceptanceImgEntity> query, AcceptanceImgFilter filter)
    {
        if (filter.AcceptanceIds != null && filter.AcceptanceIds.Any())
        {
            query = query.Where(a => filter.AcceptanceIds.Contains(a.AcceptanceId));
        }

        return query;
    }

    public async Task<List<AcceptanceImgItem>> GetPaged(AcceptanceImgFilter filter, CancellationToken ct)
    {
        var query = context.AcceptanceImgs.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = query.OrderByDescending(a => a.Id);

        return await query
            .ProjectTo<AcceptanceImgItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<AcceptanceImgItem?> GetById(long id, CancellationToken ct)
    {
        return await context.AcceptanceImgs
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ProjectTo<AcceptanceImgItem>(mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
            
    }

    public async Task<int> GetCount(AcceptanceImgFilter filter, CancellationToken ct)
    {
        var query = context.AcceptanceImgs.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<long> Create(AcceptanceImg acceptanceImg, CancellationToken ct)
    {
        var accptanceImgEntity = new AcceptanceImgEntity
        {
            AcceptanceId = acceptanceImg.AcceptanceId,
            FilePath = acceptanceImg.FilePath,
            Description = acceptanceImg.Description,
        };

        await context.AddAsync(accptanceImgEntity, ct);
        await context.SaveChangesAsync(ct);

        return accptanceImgEntity.Id;
    }

    public async Task<long> Update(long id, string? filePath, string? description, CancellationToken ct)
    {
        var entity = await context.AcceptanceImgs.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("AcceptanceImg not found");

        if (!string.IsNullOrEmpty(filePath))
        {
            entity.FilePath = filePath;
        }

        if (!string.IsNullOrEmpty(description))
        {
            entity.Description = description;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        await context.AcceptanceImgs
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
