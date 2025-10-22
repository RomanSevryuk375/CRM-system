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
        var clientEntyties = await _context.Clients
        .AsNoTracking()
        .ToListAsync();

        var clients = clientEntyties
            .Select(c => Client.Create(c.ClientId, c.ClientUserId, c.ClientName, c.ClientSurname, c.ClientPhoneNumber, c.ClientEmail).client)
            .ToList();

        return clients;
    }

    public async Task<int> Create(Client client)
    {
        var clientEntyties = new ClientEntity
        {
            ClientUserId = client.UserId,
            ClientName = client.Name,
            ClientSurname = client.Surname,
            ClientPhoneNumber = client.PhoneNumber,
            ClientEmail = client.Email
        };

        await _context.Clients.AddAsync(clientEntyties);
        await _context.SaveChangesAsync();

        return clientEntyties.ClientId;
    }

    public async Task<int> Update(int id, string name, string surname, string phoneNumber, string email)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == id);
        if (client == null)
            throw new Exception("Client not found");

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
        var clientRntyti = await _context.Clients
            .Where(c => c.ClientId == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
