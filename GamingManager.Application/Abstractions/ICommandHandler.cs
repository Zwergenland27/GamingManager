using CleanDomainValidation.Domain;
using MediatR;

namespace GamingManager.Application.Abstractions;

/// <summary>
/// Handler for command <typeparamref name="TCommand"/>
/// </summary>
/// <typeparam name="TCommand">Command of type <see cref="ICommand"/></typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, CanFail>
	where TCommand : ICommand
{
}

/// <summary>
/// Handler for command <typeparamref name="TCommand"/>, with return value of type <typeparamref name="TResponse"/>
/// </summary>
/// <typeparam name="TCommand">Command of type <see cref="ICommand{TResponse}"/></typeparam>
/// <typeparam name="TResponse">Type of the returned value</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, CanFail<TResponse>>
	where TCommand : ICommand<TResponse>;
