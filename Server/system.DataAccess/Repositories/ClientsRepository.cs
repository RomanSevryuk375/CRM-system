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
                c.ClientId,
                c.ClientUserId,
                c.ClientName,
                c.ClientSurname,
                c.ClientPhoneNumber,
                c.ClientEmail).client)
            .ToList();

        return clients;
    }

    public async Task<List<Client>> GetClientByUserId(int userId)
    {
        var clientEntities = await _context.Clients
            .Where(c => c.ClientUserId == userId)
            .AsNoTracking()
            .ToListAsync();

        var clients = clientEntities
            .Select(c => Client.Create(
                c.ClientId,
                c.ClientUserId,
                c.ClientName,
                c.ClientSurname,
                c.ClientPhoneNumber,
                c.ClientEmail).client)
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
            ClientUserId = client.UserId,
            ClientName = client.Name,
            ClientSurname = client.Surname,
            ClientPhoneNumber = client.PhoneNumber,
            ClientEmail = client.Email
        };

        await _context.Clients.AddAsync(clientEntities);
        await _context.SaveChangesAsync();

        return clientEntities.ClientId;
    }

    public async Task<int> Update(int id, string name, string surname, string phoneNumber, string email)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == id)
            ?? throw new Exception("Client not found");

        if (!string.IsNullOrWhiteSpace(name))
            client.ClientName = name;

        if (!string.IsNullOrWhiteSpace(surname))
            client.ClientSurname = surname;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            client.ClientPhoneNumber = phoneNumber;

        if (!string.IsNullOrWhiteSpace(email))
            client.ClientEmail = email;

        await _context.SaveChangesAsync();

        return client.ClientId;
    }

    public async Task<int> Delete(int id)
    {
        var clientEntity = await _context.Clients
            .Where(c => c.ClientId == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
