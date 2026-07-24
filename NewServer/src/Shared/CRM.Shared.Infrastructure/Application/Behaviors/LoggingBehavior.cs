using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CRM.Shared.Infrastructure.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        Stopwatch stopwatch = Stopwatch.StartNew();

        TResponse response = await next();

        stopwatch.Stop();

        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds} ms",
            requestName, elapsedMilliseconds);

        if (elapsedMilliseconds > 500)
        {
            logger.LogWarning("Long running request: {RequestName} ({ElapsedMilliseconds} ms)",
                requestName, elapsedMilliseconds);
        }

        return response;
    }
}
