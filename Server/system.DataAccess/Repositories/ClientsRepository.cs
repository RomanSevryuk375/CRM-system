using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ClientsRepository : IClientRepository
{
    private readonly SystemDbContext _context;

    public ClientsRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<ClientEntity> ApplyFilter(IQueryable<ClientEntity> query, ClientFilter filter)
    {
        if (filter.UserIds != null && filter.UserIds.Any())
            query = query.Where(c => filter.UserIds.Contains(c.UserId));

        return query;
    }

    public async Task<List<ClientItem>> GetPaged(ClientFilter filter)
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

        var projection = query.Select(c => new ClientItem(
            c.Id,
            c.UserId,
            c.Name,
            c.Surname,
            c.PhoneNumber,
            c.Email));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(ClientFilter filter)
    {
        var query = _context.Clients.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<ClientItem?> GetById(long id)
    {
        var clientItem = await _context.Clients
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ClientItem(
            c.Id,
            c.UserId,
            c.Name,
            c.Surname,
            c.PhoneNumber,
            c.Email))
            .FirstOrDefaultAsync();

        return clientItem;
    }

    public async Task<long> Create(Client client)
    {
        var clientEntities = new ClientEntity
        {
            UserId = client.UserId,
            Name = client.Name,
            Surname = client.Surname,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email
        };

        await _context.Clients.AddAsync(clientEntities);
        await _context.SaveChangesAsync();

        return clientEntities.Id;
    }

    public async Task<long> Update(long id, ClientUpdateModel model)
    {
        var entity = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new Exception("Client not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Surname)) entity.Surname = model.Surname;
        if (!string.IsNullOrWhiteSpace(model.PhoneNumber)) entity.PhoneNumber = model.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(model.Email)) entity.Email = model.Email;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var clientEntity = await _context.Clients
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Clients
            .AsNoTracking()
            .AnyAsync(c => c.Id == id);
    }
}
