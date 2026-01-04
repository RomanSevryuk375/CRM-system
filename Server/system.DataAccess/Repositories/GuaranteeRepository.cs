using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.Guarantee;
using CRMSystem.DataAccess.Entites;
using CRMSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class GuaranteeRepository : IGuaranteeRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public GuaranteeRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<GuaranteeEntity> ApplyFilter(IQueryable<GuaranteeEntity> query, GuaranteeFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(g => filter.OrderIds.Contains(g.OrderId));

        return query;
    }

    public async Task<List<GuaranteeItem>> GetPaged(GuaranteeFilter filter)
    {
        var query = _context.Guarantees.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "datestart" => filter.IsDescending
                ? query.OrderByDescending(g => g.DateStart)
                : query.OrderBy(g => g.DateStart),
            "dateend" => filter.IsDescending
                ? query.OrderByDescending(g => g.DateEnd)
                : query.OrderBy(g => g.DateStart),
            "terms" => filter.IsDescending
                ? query.OrderByDescending(g => g.Terms)
                : query.OrderBy(g => g.Terms),
            "description" => filter.IsDescending
                ? query.OrderByDescending(g => g.Description)
                : query.OrderBy(g => g.Description),

            _ => filter.IsDescending
                ? query.OrderByDescending(g => g.Id)
                : query.OrderBy(g => g.Id),
        };

        return await query
            .ProjectTo<GuaranteeItem>(_mapper.ConfigurationProvider)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(GuaranteeFilter filter)
    {
        var query = _context.Guarantees.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Guarantee guarantee)
    {
        var guarantyEntity = new GuaranteeEntity
        {
            OrderId = guarantee.OrderId,
            DateStart = guarantee.DateStart,
            DateEnd = guarantee.DateEnd,
            Description = guarantee.Description,
            Terms = guarantee.Terms
        };

        await _context.Guarantees.AddAsync(guarantyEntity);
        await _context.SaveChangesAsync();

        return guarantyEntity.Id;
    }

    public async Task<long> Update(long id, GuaranteeUpdateModel model)
    {
        var entity = await _context.Guarantees.FirstOrDefaultAsync(g => g.Id == id)
            ?? throw new Exception("Guarantee not found");

        if (!string.IsNullOrWhiteSpace(model.Description)) entity.Description = model.Description;
        if (!string.IsNullOrWhiteSpace(model.Terms)) entity.Description = model.Terms;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Guarantees
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
