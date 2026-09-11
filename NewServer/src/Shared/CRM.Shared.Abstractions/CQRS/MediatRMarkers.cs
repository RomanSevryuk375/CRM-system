using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Shared.Abstractions.CQRS;

public interface IBaseCommand { }

public interface ICommand : IRequest<Result>, IBaseCommand { }

public interface ICommand<TValue> : IRequest<Result<TValue>>, IBaseCommand { }

public interface IQuery<out TResponse> : IRequest<TResponse> { }
