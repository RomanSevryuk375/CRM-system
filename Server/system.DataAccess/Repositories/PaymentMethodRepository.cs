using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public PaymentMethodRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PaymentMethodItem>> Get()
    {
        return await _context.PaymentMethods
            .AsNoTracking()
            .ProjectTo<PaymentMethodItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.PaymentMethods
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }
}
