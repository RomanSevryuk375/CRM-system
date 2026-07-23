using CRM.Shared.Abstractions.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CRM.Shared.Infrastructure.OutboxMessages;

public sealed class OutboxMessageProcessorService<TDbContext>(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<OutboxMessageProcessorService<TDbContext>> logger)
    where TDbContext : DbContext
{
    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IOutboxRepository<TDbContext> outboxRepository = scope.ServiceProvider
            .GetRequiredService<IOutboxRepository<TDbContext>>();
        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        IReadOnlyList<OutboxMessage> messages = await outboxRepository.GetPendingMessagesAsync(50, cancellationToken);
        if (messages.Count == 0)
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

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}