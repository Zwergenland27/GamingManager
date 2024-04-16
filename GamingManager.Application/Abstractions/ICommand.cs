using CleanDomainValidation.Domain;
using MediatR;

namespace GamingManager.Application.Abstractions;

/// <summary>
/// CQRS Command without return value
/// </summary>
public interface ICommand : IRequest<CanFail>, CleanDomainValidation.Application.IRequest
{
}

/// <summary>
/// CQRS Command with return value of type <typeparamref name="TResponse"/>
/// </summary>
public interface ICommand<TResponse> : IRequest<CanFail<TResponse>>, CleanDomainValidation.Application.IRequest
{
}
