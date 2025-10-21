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
        var clientEntity = await _context.Clients
            .Where(c => c.ClientId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.ClientName, c => name)
                .SetProperty(c => c.ClientSurname, c => surname)
                .SetProperty(c => c.ClientPhoneNumber, c => phoneNumber)
                .SetProperty(c => c.ClientEmail, c => email));

        return id;
    }

    public async Task<int> Delete(int id)
    {
        var clientRntyti = await _context.Clients
            .Where(c => c.ClientId == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
