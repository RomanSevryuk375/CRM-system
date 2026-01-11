using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class ClientsRepository : IClientRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public ClientsRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<ClientEntity> ApplyFilter(IQueryable<ClientEntity> query, ClientFilter filter)
    {
        if (filter.UserIds != null && filter.UserIds.Any())
            query = query.Where(c => filter.UserIds.Contains(c.UserId));

        return query;
    }

    public async Task<List<ClientItem>> GetPaged(ClientFilter filter, CancellationToken ct)
    {
        var query = _context.Clients.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "name" => filter.IsDescending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name),
            "surname" => filter.IsDescending
                ? query.OrderByDescending(c => c.Surname)
                : query.OrderBy(c => c.Surname),
            "phonenumber" => filter.IsDescending
                ? query.OrderByDescending(c => c.PhoneNumber)
                : query.OrderBy(c => c.PhoneNumber),
            "email" => filter.IsDescending
                ? query.OrderByDescending(c => c.Email)
                : query.OrderBy(c => c.Email),

            _ => filter.IsDescending
                ? query.OrderByDescending(c => c.Id)
                : query.OrderBy(c => c.Id),
        };

        return await query
            .ProjectTo<ClientItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(ClientFilter filter, CancellationToken ct)
    {
        var query = _context.Clients.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<ClientItem?> GetById(long id, CancellationToken ct)
    {
        return await _context.Clients
            .AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectTo<ClientItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<long> Create(Client client, CancellationToken ct)
    {
        var clientEntities = new ClientEntity
        {
            UserId = client.UserId,
            Name = client.Name,
            Surname = client.Surname,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email
        };

        await _context.Clients.AddAsync(clientEntities, ct);
        await _context.SaveChangesAsync(ct);

        return clientEntities.Id;
    }

    public async Task<long> Update(long id, ClientUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct)
            ?? throw new NotFoundException("Client not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Surname)) entity.Surname = model.Surname;
        if (!string.IsNullOrWhiteSpace(model.PhoneNumber)) entity.PhoneNumber = model.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(model.Email)) entity.Email = model.Email;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var clientEntity = await _context.Clients
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.Clients
            .AsNoTracking()
            .AnyAsync(c => c.Id == id, ct);
    }
}
