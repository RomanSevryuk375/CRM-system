using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AcceptanceImgRepository : IAcceptanceImgRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public AcceptanceImgRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<AcceptanceImgEntity> ApplyFilter(IQueryable<AcceptanceImgEntity> query, AcceptanceImgFilter filter)
    {
        if (filter.AcceptanceIds != null && filter.AcceptanceIds.Any())
            query = query.Where(a => filter.AcceptanceIds.Contains(a.AcceptanceId));

        return query;
    }

    public async Task<List<AcceptanceImgItem>> GetPaged(AcceptanceImgFilter filter)
    {
        var query = _context.AcceptanceImgs.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = query.OrderByDescending(a => a.Id);

        return await query
            .ProjectTo<AcceptanceImgItem>(_mapper.ConfigurationProvider)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<AcceptanceImgItem?> GetById(long id)
    {
        return await _context.AcceptanceImgs
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ProjectTo<AcceptanceImgItem>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            
    }

    public async Task<int> GetCount(AcceptanceImgFilter filter)
    {
        var query = _context.AcceptanceImgs.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(AcceptanceImg acceptanceImg)
    {
        var accptanceImgEntity = new AcceptanceImgEntity
        {
            AcceptanceId = acceptanceImg.AcceptanceId,
            FilePath = acceptanceImg.FilePath,
            Description = acceptanceImg.Description,
        };

        await _context.AddAsync(accptanceImgEntity);
        await _context.SaveChangesAsync();

        return accptanceImgEntity.Id;
    }

    public async Task<long> Update(long id, string? filePath, string? description)
    {
        var entity = await _context.AcceptanceImgs.FirstOrDefaultAsync(a => a.Id == id) 
            ?? throw new Exception("AcceptanceImg not found");

        if (entity == null) throw new Exception("AcceptanceImg not found");

        if (!string.IsNullOrEmpty(filePath)) entity.FilePath = filePath;
        if (!string.IsNullOrEmpty(description)) entity.Description = description;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.AcceptanceImgs
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
