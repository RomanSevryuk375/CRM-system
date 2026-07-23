using CRM.Shared.Abstractions.DDD;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM.Shared.Infrastructure.Data.InboxMessages;

public sealed class IdempotentDomainEventHandler<TEvent, TDbContext>(
    INotificationHandler<TEvent> decorated,
    TDbContext dbContext,
    ILogger<IdempotentDomainEventHandler<TEvent, TDbContext>> logger)
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
    where TDbContext : DbContext
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        string eventName = notification.GetType().Name;

        bool alreadyProcessed = await dbContext.Set<InboxMessage>()
            .AnyAsync(x => x.Id == notification.EventId, cancellationToken);

        if (alreadyProcessed)
        {
            logger.LogInformation("Event {EventName} with ID {EventId} was already processed. Skipping.",
                eventName, notification.EventId);
            return;
        }

        await decorated.Handle(notification, cancellationToken);

        InboxMessage inboxMessage = new()
        {
            Id = notification.EventId,
            Name = eventName,
            ProcessedOnUtc = DateTime.UtcNow
        };

        dbContext.Set<InboxMessage>().Add(inboxMessage);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}