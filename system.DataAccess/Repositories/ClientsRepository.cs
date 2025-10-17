using Microsoft.EntityFrameworkCore;
using system.Core.Models;
using system.DataAccess;
using system.DataAccess.Entites;
using system.DataAccess.Repositories;

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
        var clientEntyties = await _context.Сlients
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

        await _context.Сlients.AddAsync(clientEntyties);
        await _context.SaveChangesAsync();

        return clientEntyties.ClientId;
    }


}
