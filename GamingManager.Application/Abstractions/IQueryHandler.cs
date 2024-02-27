using CleanDomainValidation.Domain;
using MediatR;

namespace GamingManager.Application.Abstractions;

/// <summary>
/// Handler for the query <typeparamref name="TQuery"/>, with return value of type <typeparamref name="TResponse"/>
/// </summary>
/// <typeparam name="TQuery">Request of type <see cref="IQuery{TResponse}"/></typeparam>
/// <typeparam name="TResponse">Type of the returned value</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, CanFail<TResponse>>
	where TQuery : IQuery<TResponse>;
