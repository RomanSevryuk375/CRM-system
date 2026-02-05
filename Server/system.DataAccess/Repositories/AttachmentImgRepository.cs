// Ignore Spelling: Img

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class AttachmentImgRepository(
    SystemDbContext context,
    IMapper mapper) : IAttachmentImgRepository
{
    private static IQueryable<AttachmentImgEntity> ApplyFilter(IQueryable<AttachmentImgEntity> query, AttachmentImgFilter filter)
    {
        if (filter.AttachmentIds != null && filter.AttachmentIds.Any())
        {
            query = query.Where(a => filter.AttachmentIds.Contains(a.AttachmentId));
        }

        return query;
    }

    public async Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter, CancellationToken ct)
    {
        var query = context.AttachmentImgs.AsNoTracking();

        query = ApplyFilter(query, filter).OrderByDescending(a => a.Id);

        return await query
            .ProjectTo<AttachmentImgItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<AttachmentImgItem?> GetById(long  id, CancellationToken ct)
    {
        return await context.AttachmentImgs
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ProjectTo<AttachmentImgItem>(mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetCount(AttachmentImgFilter filter, CancellationToken ct)
    {
        var query = context.AttachmentImgs.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<long> Create(AttachmentImg attachmentImg, CancellationToken ct)
    {
        var attachmentImgEntity = new AttachmentImgEntity
        {
            AttachmentId = attachmentImg.AttachmentId,
            FilePath = attachmentImg.FilePath,
            Description = attachmentImg.Description,
        };

        await context.AddAsync(attachmentImgEntity, ct);
        await context.SaveChangesAsync(ct);

        return attachmentImgEntity.Id;
    }

    public async Task<long> Update(long id, string? filePath, string? description, CancellationToken ct)
    {
        var entity = await context.AttachmentImgs.FirstOrDefaultAsync(a => a.Id == id, ct) 
            ?? throw new NotFoundException("AttachmentImg not found");

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
        await context.AttachmentImgs
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
