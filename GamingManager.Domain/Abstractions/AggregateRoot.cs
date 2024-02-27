namespace GamingManager.Domain.Abstractions;

/// <summary>
/// Root of an aggregate which manipulates its entities
/// </summary>
/// <typeparam name="TId">Id of the aggregate of type <typeparamref name="TIdType"/></typeparam>
/// <typeparam name="TIdType">Type of the aggregat id</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>
	where TId : notnull
{
	/// <summary>
	/// Creates aggregate root instance with Id <paramref name="id"/>
	/// </summary>
	/// <param name="id">Id of the aggregate root</param>  
	protected AggregateRoot(TId id) : base(default!)
	{
		Id = id;
	}
}
