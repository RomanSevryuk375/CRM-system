using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public AttachmentRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<AttachmentEntity> ApplyFilter(IQueryable<AttachmentEntity> query, AttachmentFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(a => filter.WorkerIds.Contains(a.WorkerId));

        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(a => filter.OrderIds.Contains(a.OrderId));

        if (filter.AttachmentIds != null && filter.AttachmentIds.Any())
            query = query.Where(a => filter.AttachmentIds.Contains(a.Id));

        return query;
    }

    public async Task<List<AttachmentItem>> GetPaged(AttachmentFilter filter, CancellationToken ct)
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

        return await query
            .ProjectTo<AttachmentItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(AttachmentFilter filter, CancellationToken ct)
    {
        var query = _context.Attachments.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Attachment attachment, CancellationToken ct)
    {
        var attachmentEntity = new AttachmentEntity
        {
            OrderId = attachment.OrderId,
            WorkerId = attachment.WorkerId,
            CreatedAt = attachment.CreatedAt,
            Description = attachment.Description,
        };

        await _context.Attachments.AddAsync(attachmentEntity, ct);
        await _context.SaveChangesAsync(ct);

        return attachmentEntity.Id;
    }

    public async Task<long> Update(long id, string? description, CancellationToken ct)
    {
        var entity = await _context.Attachments.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("Attachment not found");

        if (!string.IsNullOrEmpty(description)) entity.Description = description;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.Attachments
            .Where(aa => aa.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists (long id, CancellationToken ct)
    {
        return await _context.Attachments
            .AsNoTracking()
            .AnyAsync(a => a.Id == id, ct);
    }
}
