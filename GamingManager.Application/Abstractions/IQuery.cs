using CleanDomainValidation.Domain;
using MediatR;

namespace GamingManager.Application.Abstractions;

/// <summary>
/// CQRS Query, with return value of type <typeparamref name="TResponse"/>
/// </summary>
/// <typeparam name="TResponse">Typ des Rückgabewertes</typeparam>
public interface IQuery<TResponse> : IRequest<CanFail<TResponse>>, CleanDomainValidation.Application.IRequest
{
}
