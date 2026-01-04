using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class CarStatusRepository : ICarStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public CarStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CarStatusItem>> Get()
    {
        return await _context.CarStatuses
            .AsNoTracking()
            .ProjectTo<CarStatusItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(long id)
    {
        var exists = await _context.CarStatuses.AnyAsync(c => c.Id == id);
        Console.WriteLine($"DEBUG: Checking status {id}. Exists in DB? {exists}");
        return exists;
    }
}
