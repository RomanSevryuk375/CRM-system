using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

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

    private static IQueryable<GuaranteeEntity> ApplyFilter(IQueryable<GuaranteeEntity> query, GuaranteeFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(g => filter.OrderIds.Contains(g.OrderId));

        return query;
    }

    public async Task<List<GuaranteeItem>> GetPaged(GuaranteeFilter filter, CancellationToken ct)
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
            .ProjectTo<GuaranteeItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(GuaranteeFilter filter, CancellationToken ct)
    {
        var query = _context.Guarantees.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Guarantee guarantee, CancellationToken ct)
    {
        var guarantyEntity = new GuaranteeEntity
        {
            OrderId = guarantee.OrderId,
            DateStart = guarantee.DateStart,
            DateEnd = guarantee.DateEnd,
            Description = guarantee.Description,
            Terms = guarantee.Terms
        };

        await _context.Guarantees.AddAsync(guarantyEntity, ct);
        await _context.SaveChangesAsync(ct);

        return guarantyEntity.Id;
    }

    public async Task<long> Update(long id, GuaranteeUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Guarantees.FirstOrDefaultAsync(g => g.Id == id, ct)
            ?? throw new NotFoundException("Guarantee not found");

        if (!string.IsNullOrWhiteSpace(model.Description)) entity.Description = model.Description;
        if (!string.IsNullOrWhiteSpace(model.Terms)) entity.Description = model.Terms;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.Guarantees
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
