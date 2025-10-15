using Microsoft.EntityFrameworkCore;
using system.Core.Models;
using system.DataAccess.Entites;

namespace system.DataAccess.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly SystemDbContext _context;

    public ClientsRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> Get()
    {
        var clientEntyties = await _context.clients
        .AsNoTracking()
        .ToListAsync();

        var clients = clientEntyties
            .Select(c => Client.Create(c.client_id, c.client_user_id, c.client_name, c.client_surname, c.client_phone_number, c.client_email).client)
            .ToList();

        return clients;
    }

    public async Task<int> Create(Client client)
    {
        var clientEntyties = new ClientEntity
        {
            client_id = client.Id,
            client_user_id = client.UserId,
            client_name = client.Name,
            client_surname = client.Surname,
            client_phone_number = client.PhoneNumber,
            client_email = client.Email
        };

        await _context.clients.AddAsync(clientEntyties);
        await _context.SaveChangesAsync();

        return clientEntyties.client_id;
    }


}
