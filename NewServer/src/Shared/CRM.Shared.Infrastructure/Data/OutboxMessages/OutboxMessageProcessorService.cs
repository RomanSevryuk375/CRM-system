using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Text.Json;

namespace CRM.Shared.Infrastructure.Data.OutboxMessages;

public sealed class OutboxMessageProcessorService<TDbContext>(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<OutboxMessageProcessorService<TDbContext>> logger)
    where TDbContext : DbContext
{
    public async Task<Result> ProcessAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        IEntityType? entityType = dbContext.Model.FindEntityType(typeof(OutboxMessage));
        string schema = entityType?.GetSchema() ?? "public";
        string tableName = entityType?.GetTableName() ?? "outbox_messages";
        string fullTableName = $"\"{schema}\".\"{tableName}\"";

        await using IDbContextTransaction transaction =
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

        DbConnection connection = dbContext.Database.GetDbConnection();
        DbTransaction? dbTransaction = dbContext.Database.CurrentTransaction?.GetDbTransaction();

        string selectSql = $"""
            SELECT 
              id AS Id, 
              type AS Type, 
              content AS Content, 
              occurred_on_utc AS OccurredOnUtc
            FROM {fullTableName}
            WHERE processed_on_utc IS NULL
            ORDER BY occurred_on_utc
            LIMIT 50
            FOR UPDATE SKIP LOCKED
            """;

        List<OutboxMessage> messages = (await connection.QueryAsync<OutboxMessage>(
            selectSql,
            transaction: dbTransaction)).ToList();
        if (messages.Count == 0)
        {
            return Result.Success();
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
                    message.ProcessedOnUtc = DateTimeOffset.UtcNow.UtcDateTime;
                    continue;
                }

                if (JsonSerializer.Deserialize(message.Content, type) is not IDomainEvent domainEvent)
                {
                    logger.LogWarning("Content of outbox message {MessageId} is not an IDomainEvent", message.Id);
                    message.Error = "Content is not an IDomainEvent.";
                    message.ProcessedOnUtc = DateTimeOffset.UtcNow.UtcDateTime;
                    continue;
                }

                await publisher.Publish(domainEvent, cancellationToken);
                message.ProcessedOnUtc = DateTimeOffset.UtcNow.UtcDateTime;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process outbox message {MessageId}", message.Id);
                message.Error = ex.Message;
                message.ProcessedOnUtc = DateTimeOffset.UtcNow.UtcDateTime;
            }
        }

        string updateSql = $"""
            UPDATE {fullTableName}
            SET processed_on_utc = @ProcessedOnUtc, error = @Error
            WHERE id = @Id
            """;

        await connection.ExecuteAsync(
            updateSql,
            messages.Select(m => new { m.ProcessedOnUtc, m.Error, m.Id }),
            transaction: dbTransaction);

        await transaction.CommitAsync(cancellationToken);

        return Result.Success();
    }
}