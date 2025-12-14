using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AttachmentImgRepository : IAttachmentImgRepository
{
    private readonly SystemDbContext _context;

    public AttachmentImgRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<AttachmentImgEntity> ApplyFilter(IQueryable<AttachmentImgEntity> query, AttachmentImgFilter filter)
    {
        if (filter.attachmentIds != null && filter.attachmentIds.Any())
            query = query.Where(a => filter.attachmentIds.Contains(a.AttachmentId));

        return query;
    }

    public async Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter)
    {
        var query = _context.AttachmentImgs.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = query.OrderByDescending(a => a.Id);

        var projection = query.Select(a => new AttachmentImgItem(
            a.Id,
            a.AttachmentId,
            a.FilePath,
            a.Description));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<long> GetCount(AttachmentImgFilter filter)
    {
        var query = _context.AttachmentImgs.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(AttachmentImg attachmentImg)
    {
        var attachmentImgEntity = new AttachmentImgEntity
        {
            AttachmentId = attachmentImg.AttachmentId,
            FilePath = attachmentImg.FilePath,
            Description = attachmentImg.Description,
        };

        await _context.AddAsync(attachmentImgEntity);
        await _context.SaveChangesAsync();

        return attachmentImgEntity.Id;
    }

    public async Task<long> Update(long id, string? filePath, string? description)
    {
        var entity = await _context.AttachmentImgs
            .FirstOrDefaultAsync(a => a.Id == id);

        if (entity == null) throw new Exception("AttachmentImg not found");

        if (!string.IsNullOrEmpty(filePath)) entity.FilePath = filePath;
        if (!string.IsNullOrEmpty(description)) entity.Description = description;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.AttachmentImgs
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
