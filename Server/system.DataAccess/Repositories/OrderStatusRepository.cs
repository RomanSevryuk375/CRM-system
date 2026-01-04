using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public OrderStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderStatusItem>> Get()
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .ProjectTo<OrderStatusItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .AnyAsync(o => o.Id == id);
    }
}
