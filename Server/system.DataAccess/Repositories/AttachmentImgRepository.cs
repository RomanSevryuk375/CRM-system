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

public class AttachmentImgRepository : IAttachmentImgRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public AttachmentImgRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<AttachmentImgEntity> ApplyFilter(IQueryable<AttachmentImgEntity> query, AttachmentImgFilter filter)
    {
        if (filter.AttachmentIds != null && filter.AttachmentIds.Any())
            query = query.Where(a => filter.AttachmentIds.Contains(a.AttachmentId));

        return query;
    }

    public async Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter, CancellationToken ct)
    {
        var query = _context.AttachmentImgs.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = query.OrderByDescending(a => a.Id);

        return await query
            .ProjectTo<AttachmentImgItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<AttachmentImgItem?> GetById(long  id, CancellationToken ct)
    {
        return await _context.AttachmentImgs
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ProjectTo<AttachmentImgItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetCount(AttachmentImgFilter filter, CancellationToken ct)
    {
        var query = _context.AttachmentImgs.AsNoTracking();
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

        await _context.AddAsync(attachmentImgEntity, ct);
        await _context.SaveChangesAsync(ct);

        return attachmentImgEntity.Id;
    }

    public async Task<long> Update(long id, string? filePath, string? description, CancellationToken ct)
    {
        var entity = await _context.AttachmentImgs.FirstOrDefaultAsync(a => a.Id == id, ct) 
            ?? throw new NotFoundException("AttachmentImg not found");

        if (!string.IsNullOrEmpty(filePath)) entity.FilePath = filePath;
        if (!string.IsNullOrEmpty(description)) entity.Description = description;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.AttachmentImgs
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
