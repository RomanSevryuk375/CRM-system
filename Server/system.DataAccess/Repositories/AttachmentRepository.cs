using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly SystemDbContext _context;

    public AttachmentRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<AttachmentEntity> ApplyFilter(IQueryable<AttachmentEntity> query, AttachmentFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(a => filter.WorkerIds.Contains(a.WorkerId));

        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(a => filter.OrderIds.Contains(a.OrderId));

        return query;
    }

    public async Task<List<AttachmentItem>> GetPaged(AttachmentFilter filter)
    {
        var query = _context.Attachments.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "createat" => filter.IsDescending
                ? query.OrderByDescending(a => a.CreatedAt)
                : query.OrderBy(a => a.CreatedAt),

            _ => filter.IsDescending
                ? query.OrderByDescending(a => a.Id)
                : query.OrderBy(a => a.Id),
        };

        var projection = query.Select(a => new AttachmentItem(
            a.Id,
            a.OrderId,
            a.Worker == null
                ? ""
                : $"{a.Worker.Name} {a.Worker.Surname}",
            a.CreatedAt,
            a.Description));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<long> GetCount(AttachmentFilter filter)
    {
        var query = _context.Attachments.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Attachment attachment)
    {
        var attachmentEntity = new AttachmentEntity
        {
            OrderId = attachment.OrderId,
            WorkerId = attachment.WorkerId,
            CreatedAt = attachment.CreatedAt,
            Description = attachment.Description,
        };

        await _context.Attachments.AddAsync(attachmentEntity);
        await _context.SaveChangesAsync();

        return attachmentEntity.Id;
    }

    public async Task<long> Update(long id, string? description)
    {
        var entity = await _context.Attachments
            .FirstOrDefaultAsync(a => a.Id == id);

        if (entity == null) throw new Exception("Attachment not found");

        if (!string.IsNullOrEmpty(description)) entity.Description = description;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Attachments
            .Where(aa => aa.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
