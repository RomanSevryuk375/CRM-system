using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Shared.Abstractions.CQRS;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;
