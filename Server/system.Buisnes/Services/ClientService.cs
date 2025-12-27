using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Client;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ClientService> _logger;

    public ClientService(
        IClientRepository clientRepository,
        IUserRepository userRepository,
        ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<ClientItem>> GetPagedCkients(ClientFilter filter)
    {
        _logger.LogInformation("Getting client start");

        var client = await _clientRepository.GetPaged(filter);

        _logger.LogInformation("Getting client success");

        return client;
    }

    public async Task<int> GetCountClients(ClientFilter filter)
    {
        _logger.LogInformation("Getting count client start");

        var count = await _clientRepository.GetCount(filter);

        _logger.LogInformation("Getting count client success");

        return count;
    }

    public async Task<ClientItem> GetClientById(long id)
    {
        _logger.LogInformation("Getting count client start");

        var client = await _clientRepository.GetById(id);
        if (client is null)
        {
            _logger.LogError("Client{ClinetId} not found", id);
            throw new NotFoundException($"Client{id} not found");
        }

        _logger.LogInformation("Getting count client success");

        return client;
    }

    public async Task<long> CreateClient(Client client)
    {
        _logger.LogInformation("Creating client start");

        if (!await _userRepository.Exists(client.UserId))
        {
            _logger.LogError("User{UserId} not found", client.UserId);
            throw new NotFoundException($"User{client.UserId} not found");
        }

        var Id = await _clientRepository.Create(client);

        _logger.LogInformation("Creating client success");

        return Id;
    }

    public async Task<long> CreateClientWithUser(Client client, User user)
    {
        var userId = 0L;
        try
        {
            _logger.LogInformation("Creating user start");

            userId = await _userRepository.Create(user);
            client.SetUserId(userId);

            var Id = await _clientRepository.Create(client);

            _logger.LogInformation("Creating client success");

            return Id;
        }
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Failed to create client. Rolling back user.");
            if (userId > 0)
                await _userRepository.Delete(userId);
            throw;
        }
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model)
    {
        _logger.LogInformation("Updating client start");

        var Id = await _clientRepository.Update(id, model);

        _logger.LogInformation("Updating client success");

        return Id;
    }

    public async Task<long> DeleteClient(long id)
    {
        _logger.LogInformation("Deleting client start");

        var Id = await _clientRepository.Delete(id);

        _logger.LogInformation("Deleting client success");

        return Id;
    }
}
