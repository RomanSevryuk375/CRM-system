using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Shared.Infrastructure.Application.Behaviors;

public sealed class TransactionBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IBaseCommand)
        {
            return await next();
        }

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            TResponse? response = await next();

            if (response is Result { IsFailure: true })
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                return response;
            }

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return response;
        }
        catch (DbUpdateConcurrencyException)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);

            Error error = Error.Conflict(
                "Concurrency.Conflict",
                "The record was modified by another process. Please retry your action.");

            return BehaviorHelpers.CreateFailedResult<TResponse>(error);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
