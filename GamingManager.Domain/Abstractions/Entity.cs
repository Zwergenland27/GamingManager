namespace GamingManager.Domain.Abstractions;

/// <summary>
/// Entity 
/// </summary>
/// <typeparam name="TId">Id of the entity</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
	where TId : notnull
{
	private readonly List<IDomainEvent> _domainEvents = [];

	/// <summary>
	/// Creates new Instance of entity with id <paramref name="id"/>
	/// </summary>
	/// <param name="id">Id of the entity</param>
	protected Entity(TId id)
	{
		Id = id;
	}

	/// <summary>
	/// Unique id in the db
	/// </summary>
	public TId Id { get; protected init; }

	/// <inheritdoc/>
	public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();

	/// <inheritdoc/>
	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

	/// <inheritdoc/>
	protected void RaiseDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}

	/// <summary>
	/// Checks, wether this entity is equal another object
	/// </summary>
	/// <param name="obj">other object</param>
	/// <returns>true, if id is equal</returns>
	public override bool Equals(object? obj)
	{
		return obj is Entity<TId> entity && Id.Equals(entity.Id);
	}

	/// <summary>
	/// checks, wether this entity is equal another entity
	/// </summary>
	/// <param name="other">other entity</param>
	/// <returns>true, if id is equal</returns>
	public bool Equals(Entity<TId>? other)
	{
		return Equals((object?)other);
	}

	/// <summary>
	/// checks, wether two entities are equal
	/// </summary>
	/// <param name="left">left entity</param>
	/// <param name="right">right entity</param>
	/// <returns>true, if id is equal</returns>
	public static bool operator ==(Entity<TId> left, Entity<TId> right)
	{
		return Equals(left, right);
	}

	/// <summary>
	/// checks, wether two entities are unequal
	/// </summary>
	/// <param name="left">left entity</param>
	/// <param name="right">right entity</param>
	/// <returns>true, if id is unequal</returns>
	public static bool operator !=(Entity<TId> left, Entity<TId> right)
	{
		return !Equals(left, right);
	}

	///<inheritdoc/>
	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}
}
