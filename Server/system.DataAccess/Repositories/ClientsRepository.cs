using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly SystemDbContext _context;

    public ClientsRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> Get()
    {
        var clientEntities = await _context.Clients
        .AsNoTracking()
        .ToListAsync();

        var clients = clientEntities
            .Select(c => Client.Create(
                c.Id,
                c.UserId,
                c.Name,
                c.Surname,
                c.PhoneNumber,
                c.Email).client)
            .ToList();

        return clients;
    }

    public async Task<List<Client>> GetPaged(int page, int limit)
    {
        var clientEntities = await _context.Clients
        .AsNoTracking()
        .Skip((page - 1) * limit)
        .Take(limit)
        .ToListAsync();

        var clients = clientEntities
            .Select(c => Client.Create(
                c.Id,
                c.UserId,
                c.Name,
                c.Surname,
                c.PhoneNumber,
                c.Email).client)
            .ToList();

        return clients;
    }

    public async Task<int> GetCount()
    {
        return await _context.Clients.CountAsync();
    }

    public async Task<List<Client>> GetClientByUserId(int userId)
    {
        var clientEntities = await _context.Clients
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        var clients = clientEntities
            .Select(c => Client.Create(
                c.Id,
                c.UserId,
                c.Name,
                c.Surname,
                c.PhoneNumber,
                c.Email).client)
            .ToList();

        return clients;
    }

    public async Task<int> Create(Client client)
    {
        var (_, error) = Client.Create(
        0,
        client.UserId,
        client.Name,
        client.Surname,
        client.PhoneNumber,
        client.Email);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Client: {error}");

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

    public async Task<int> Update(int id, string? name, string? surname, string? phoneNumber, string? email)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new Exception("Client not found");

        if (!string.IsNullOrWhiteSpace(name))
            client.Name = name;

        if (!string.IsNullOrWhiteSpace(surname))
            client.Surname = surname;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            client.PhoneNumber = phoneNumber;

        if (!string.IsNullOrWhiteSpace(email))
            client.Email = email;

        await _context.SaveChangesAsync();

        return client.Id;
    }

    public async Task<int> Delete(int id)
    {
        var clientEntity = await _context.Clients
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
