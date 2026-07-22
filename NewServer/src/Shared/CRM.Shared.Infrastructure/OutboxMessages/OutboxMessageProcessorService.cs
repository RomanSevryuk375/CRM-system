using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CRM.Shared.Infrastructure.OutboxMessages;

public sealed class OutboxMessageProcessorService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<OutboxMessageProcessorService> logger)
{
    private const int BatchSize = 50;

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IOutboxRepository outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        IReadOnlyList<OutboxMessage>? messages = await outboxRepository.GetPendingMessagesAsync(
            BatchSize, cancellationToken);
        if (messages is null || !messages.Any())
        {
            return;
        }

        foreach (OutboxMessage message in messages)
        {
            try
            {
                Type? type = Type.GetType(message.Type);
                if (type is null)
                {
                    logger.LogWarning("Type {MessageType} not found for outbox message {MessageId}",
                        message.Type, message.Id);
                    message.Error = $"Type {message.Type} not found.";
                    message.ProcessedOnUtc = DateTime.UtcNow;
                    continue;
                }

                if (JsonSerializer.Deserialize(message.Content, type) is not IDomainEvent domainEvent)
                {
                    logger.LogWarning("Content of outbox message {MessageId} is not an IDomainEvent", message.Id);
                    message.Error = "Content is not an IDomainEvent.";
                    message.ProcessedOnUtc = DateTime.UtcNow;
                    continue;
                }

                await publisher.Publish(domainEvent, cancellationToken);
                message.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process outbox message {MessageId}", message.Id);
                message.Error = ex.Message;
                message.ProcessedOnUtc = DateTime.UtcNow;
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}