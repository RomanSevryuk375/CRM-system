using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ClientService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(
        IClientRepository clientRepository,
        IUserRepository userRepository,
        ILogger<ClientService> logger,
        IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _userRepository = userRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ClientItem>> GetPagedClients(ClientFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting client start");

        var client = await _clientRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting client success");

        return client;
    }

    public async Task<int> GetCountClients(ClientFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count client start");

        var count = await _clientRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count client success");

        return count;
    }

    public async Task<ClientItem> GetClientById(long id, CancellationToken ct)
    {
        _logger.LogInformation("Getting count client start");

        var client = await _clientRepository.GetById(id, ct);
        if (client is null)
        {
            _logger.LogError("Client{ClinetId} not found", id);
            throw new NotFoundException($"Client{id} not found");
        }

        _logger.LogInformation("Getting count client success");

        return client;
    }

    public async Task<long> CreateClient(Client client, CancellationToken ct)
    {
        _logger.LogInformation("Creating client start");

        if (!await _userRepository.Exists(client.UserId, ct))
        {
            _logger.LogError("User{UserId} not found", client.UserId);
            throw new NotFoundException($"User{client.UserId} not found");
        }

        var Id = await _clientRepository.Create(client, ct);

        _logger.LogInformation("Creating client success");

        return Id;
    }

    public async Task<long> CreateClientWithUser(Client client, User user, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        long userId;
        try
        {
            _logger.LogInformation("Creating user start");

            userId = await _userRepository.Create(user, ct);
            client.SetUserId(userId);

            var Id = await _clientRepository.Create(client, ct);

            _logger.LogInformation("Creating client success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdateClient(long id, ClientUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating client start");

        var Id = await _clientRepository.Update(id, model, ct);

        _logger.LogInformation("Updating client success");

        return Id;
    }

    public async Task<long> DeleteClient(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting client start");

        var Id = await _clientRepository.Delete(id, ct);

        _logger.LogInformation("Deleting client success");

        return Id;
    }
}
